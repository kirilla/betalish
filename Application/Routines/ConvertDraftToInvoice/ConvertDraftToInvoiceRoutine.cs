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

            InvoiceDate = invoiceDate,
            DueDate = dueDate,

            PaymentTermDays = draft.PaymentTermDays,
            PaymentTerms = paymentTerms,

            NetAmount = draft.NetAmount,
            VatAmount = draft.VatAmount,
            Total = draft.Total,
            TotalRounding = draft.TotalRounding,

            Customer_Address1 = draft.Customer_Address1,
            Customer_Address2 = draft.Customer_Address2,
            Customer_ZipCode = draft.Customer_ZipCode,
            Customer_City = draft.Customer_City,
            Customer_Country = draft.Customer_Country,

            Customer_Email = draft.Customer_Email,

            ClientId = draft.ClientId,
            //CustomerId = draft.CustomerId,
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

        // TODO: BalanceRows, VatRows, etc


        database.InvoiceDrafts.Remove(draft);

        await database.SaveAsync(userToken);

        return invoice.Id;
    }
}
