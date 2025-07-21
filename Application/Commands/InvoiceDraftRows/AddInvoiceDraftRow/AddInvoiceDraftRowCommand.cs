namespace Betalish.Application.Commands.InvoiceDraftRows.AddInvoiceDraftRow;

public class AddInvoiceDraftRowCommand(IDatabaseService database) : IAddInvoiceDraftRowCommand
{
    public async Task Execute(
        IUserToken userToken, AddInvoiceDraftRowCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var draft = await database.InvoiceDrafts
            .Where(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.Id == model.InvoiceDraftId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        var article = await database.Articles
            .Where(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.Id == model.ArticleId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        var row = new InvoiceDraftRow()
        {
            InvoiceDraftId = draft.Id,

            IsCredit = draft.IsCredit,

            ArticleNumber = article.Number,
            ArticleName = article.Name,
            UnitPrice = article.UnitPrice,
            Unit = article.UnitName,
            VatPercentage = article.VatValue,

            Quantity = model.Quantity!.TryParseDecimal()!.Value,

            NetAmount = 0,
            VatAmount = 0,
            TotalAmount = 0,

            RevenueAccount = article.RevenueAccount,
            VatAccount = article.VatAccount,
        };

        database.InvoiceDraftRows.Add(row);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
