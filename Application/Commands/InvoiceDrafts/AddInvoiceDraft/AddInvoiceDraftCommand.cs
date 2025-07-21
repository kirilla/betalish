namespace Betalish.Application.Commands.InvoiceDrafts.AddInvoiceDraft;

public class AddInvoiceDraftCommand(IDatabaseService database) : IAddInvoiceDraftCommand
{
    public async Task<int> Execute(
        IUserToken userToken, AddInvoiceDraftCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var customer = await database.Customers
            .AsNoTracking()
            .Where(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.Id == model.CustomerId!.Value)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        var draft = new InvoiceDraft()
        {
            ClientId = userToken.ClientId!.Value,
            CustomerId = model.CustomerId!.Value,
            IsCredit = false,
            About = model.About!,
        };

        database.InvoiceDrafts.Add(draft);

        if (model.InvoiceTemplateId.HasValue)
        {
            var template = await database.InvoiceTemplates
                .AsNoTracking()
                .Where(x =>
                    x.ClientId == userToken.ClientId!.Value &&
                    x.Id == model.InvoiceTemplateId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            var templateRows = await database.InvoiceTemplateRows
                .AsNoTracking()
                .Include(x => x.Article)
                .Where(x =>
                    x.InvoiceTemplate.ClientId == userToken.ClientId!.Value &&
                    x.InvoiceTemplateId == model.InvoiceTemplateId!.Value)
                .ToListAsync();

            var draftRows = templateRows
                .Select(x => new InvoiceDraftRow()
                {
                    InvoiceDraft = draft,

                    IsCredit = false,

                    ArticleId = x.ArticleId,
                    ArticleNumber = x.Article.Number,
                    ArticleName = x.Article.Name,
                    UnitPrice = x.Article.UnitPrice,
                    Unit = x.Article.UnitName,
                    VatPercentage = x.Article.VatValue,

                    Quantity = x.Quantity,

                    NetAmount = 0,
                    VatAmount = 0,
                    TotalAmount = 0,

                    RevenueAccount = x.Article.RevenueAccount,
                    VatAccount = x.Article.VatAccount,
                })
                .ToList();

            database.InvoiceDraftRows.AddRange(draftRows);
        }

        await database.SaveAsync(userToken);

        return draft.Id;
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
