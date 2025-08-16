namespace Betalish.Application.Routines.ConvertDraftToInvoice;

public class ConvertDraftToInvoiceRoutine(
    IDateService dateService,
    IDatabaseService database) : IConvertDraftToInvoiceRoutine
{
    public async Task<int> Execute(
        IUserToken userToken, int invoiceDraftId)
    {
        database.ChangeTracker.Clear();

        var draft = await database.InvoiceDrafts
            .Where(x =>
                x.Id == invoiceDraftId &&
                x.ClientId == userToken.ClientId!.Value)
            .SingleOrDefaultAsync() ?? 
            throw new NotFoundException();

        var draftRows = await database.InvoiceDraftRows
            .Where(x =>
                x.InvoiceDraftId == invoiceDraftId &&
                x.InvoiceDraft.ClientId == userToken.ClientId!.Value)
            .ToListAsync();

        var draftBalanceRows = await database.DraftBalanceRows
            .Include(x => x.Invoice)
            .Where(x =>
                x.InvoiceDraftId == invoiceDraftId &&
                x.InvoiceDraft.ClientId == userToken.ClientId!.Value)
            .ToListAsync();

        var terms = await database.PaymentTerms
            .Where(x =>
                x.Id == draft.PaymentTermsId &&
                x.ClientId == userToken.ClientId!.Value)
            .SingleOrDefaultAsync();

        DateOnly invoiceDate =
            draft.InvoiceDate ??
            dateService.GetDateOnlyToday();

        var invoice = new Invoice()
        {
            InvoiceKind = terms.InvoiceKind,
            InvoiceStatus = InvoiceStatus.Draft,

            InvoiceNumber = null,

            IsCredit = draft.IsCredit,

            About = draft.About,

            // Distribution
            Distribution = true,
            Reminder = terms?.Reminder ?? false,
            Demand = terms?.Demand ?? false,
            Collect = terms?.Collect ?? false,

            // Dates
            InvoiceDate = invoiceDate,
            DueDate = null,
            ReminderDate = null,
            ReminderDueDate = null,
            DemandDate = null,
            DemandDueDate = null,
            CollectDate = null,

            // Terms
            PaymentTermDays = null,
            PaymentTerms = null,

            // Summary
            NetAmount = draft.NetAmount,
            VatAmount = draft.VatAmount,
            Total = draft.Total,
            TotalRounding = draft.TotalRounding,

            // Payment
            MinToConsiderPaid = terms?.MinToConsiderPaid,

            Balance = draft.Total,
            LeftToPay = draft.Total,

            // Interest
            Interest = terms?.Interest ?? false,

            // Fees
            ReminderFee = terms?.ReminderFee,
            DemandFee = terms?.DemandFee,

            // Customer identity
            Customer_Name = draft.Customer_Name,
            CustomerKind = draft.CustomerKind,
            Customer_Ssn10 = draft.Customer_Ssn10,
            Customer_Orgnum = draft.Customer_Orgnum,

            // Customer address
            Customer_Address1 = draft.Customer_Address1,
            Customer_Address2 = draft.Customer_Address2,
            Customer_ZipCode = draft.Customer_ZipCode,
            Customer_City = draft.Customer_City,
            Customer_Country = draft.Customer_Country,

            // Customer email
            Customer_Email = draft.Customer_Email,

            // Hints
            CustomerId_Hint = draft.CustomerId_Hint,
            CustomerGuid = draft.CustomerGuid,

            // Relations
            ClientId = draft.ClientId,
        };

        if (draft.IsDebit)
        {
            invoice.DueDate =
                invoiceDate.AddDays(
                    terms?.PaymentTermDays ??
                    Defaults.Invoice.PaymentTermDays.Default);

            invoice.PaymentTermDays =
                terms?.PaymentTermDays ??
                    Defaults.Invoice.PaymentTermDays.Default;

            invoice.PaymentTerms =
                $"{invoice.PaymentTermDays} dagar netto";
        }

        database.Invoices.Add(invoice);

        var invoiceRows = draftRows
            .Select(x => new InvoiceRow()
            {
                IsCredit = x.IsCredit,
                ArticleNumber = x.ArticleNumber,
                ArticleName = x.ArticleName,
                Quantity = x.Quantity,
                UnitPrice = x.UnitPrice,
                Unit = x.Unit,
                VatRate = x.VatRate,
                NetAmount = x.NetAmount,
                VatAmount = x.VatAmount,
                TotalAmount = x.TotalAmount,
                RevenueAccount = x.RevenueAccount,
                VatAccount = x.VatAccount,
                Invoice = invoice,
            })
            .ToList();

        database.InvoiceRows.AddRange(invoiceRows);

        if (draft.IsCredit)
        {
            var balanceRows = draftBalanceRows
                .Select(x => new BalanceRow()
                {
                    DebitInvoiceNumber = x.Invoice.InvoiceNumber!.Value,
                    CreditInvoiceNumber = null,
                        // Note: The credit invoice does not yet have a number
                    Amount = x.Amount,
                    Date = invoiceDate,
                    DebitInvoiceId = x.InvoiceId,
                    CreditInvoice = invoice,
                })
                .ToList();

            database.BalanceRows.AddRange(balanceRows);
        }

        database.InvoiceDrafts.Remove(draft);

        await database.SaveAsync(userToken);

        return invoice.Id;
    }
}
