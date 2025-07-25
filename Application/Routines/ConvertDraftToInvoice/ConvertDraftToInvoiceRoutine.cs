namespace Betalish.Application.Routines.ConvertDraftToInvoice;

public class ConvertDraftToInvoiceRoutine(
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

        DateOnly invoiceDate =
            draft.InvoiceDate ??
            DateOnly.FromDateTime(DateTime.Today);

        DateOnly? dueDate;
        int? paymentTermDays;
        string paymentTerms;

        if (draft.IsCredit)
        {
            dueDate = null;
            paymentTermDays = null;
            paymentTerms = 
                draft.PaymentTerms ??
                    $"{paymentTermDays} dagar netto";
        }
        else
        {
            dueDate = 
                invoiceDate.AddDays(
                    draft.PaymentTermDays ??
                    Defaults.Invoice.PaymentTermDays.Default);

            paymentTermDays = 
                draft.PaymentTermDays ??
                    Defaults.Invoice.PaymentTermDays.Default;

            paymentTerms =
                draft.PaymentTerms ??
                    $"{paymentTermDays} dagar netto";
        }

        var invoice = new Invoice()
        {
            InvoiceStatus = InvoiceStatus.Draft,

            InvoiceNumber = null,

            IsCredit = draft.IsCredit,

            About = draft.About,

            // Dates
            InvoiceDate = invoiceDate,
            DueDate = dueDate,

            // Terms
            PaymentTermDays = draft.PaymentTermDays,
            PaymentTerms = paymentTerms,

            // Summary
            NetAmount = draft.NetAmount,
            VatAmount = draft.VatAmount,
            Total = draft.Total,
            TotalRounding = draft.TotalRounding,

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
                    CreditInvoiceNumber = 0, 
                        // TODO: The credit invoice does not yet have its number
                    Amount = x.Amount,
                    Date = invoiceDate,
                    PaymentsCreated = false, // TODO: Check this.
                    DebitInvoiceId = x.InvoiceId,
                    CreditInvoice = invoice,
                })
                .ToList();

            database.BalanceRows.AddRange(balanceRows);
        }

        // TODO: Payments, payment status, accounting?

        database.InvoiceDrafts.Remove(draft);

        await database.SaveAsync(userToken);

        return invoice.Id;
    }
}
