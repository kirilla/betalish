namespace Betalish.Application.Routines.ConvertDraftToInvoiceRoutine;

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
            .SingleOrDefaultAsync();

        if (draft == null)
            throw new NotFoundException();

        var draftRows = await database.InvoiceDraftRows
            .Where(x =>
                x.InvoiceDraftId == invoiceDraftId &&
                x.InvoiceDraft.ClientId == userToken.ClientId!.Value)
            .ToListAsync();

        var invoice = new Invoice()
        {
            InvoiceStatus = InvoiceStatus.Draft,

            InvoiceNumber = null,

            IsCredit = draft.IsCredit,

            About = draft.About,

            //NetAmount = draft.NetAmount,
            //VatAmount = draft.VatAmount,
            //Total = draft.Total,
            //TotalRounding = draft.TotalRounding,

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
