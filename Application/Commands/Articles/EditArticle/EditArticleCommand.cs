namespace Betalish.Application.Commands.Articles.EditArticle;

public class EditArticleCommand(IDatabaseService database) : IEditArticleCommand
{
    public async Task Execute(
        IUserToken userToken, EditArticleCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        if (await database.Articles
            .AnyAsync(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.Number == model.Number!.Value &&
                x.Id != model.Id))
            throw new BlockedByNumberException();

        var article = await database.Articles
            .Where(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        article.Number = model.Number!.Value;
        article.Name = model.Name!;
        article.UnitPrice = model.UnitPrice!.TryParseDecimal()!.Value;
        article.VatRate = model.VatRate!.TryParseDecimal()!.Value;
        article.UnitName = model.UnitName!;
        article.RevenueAccount = model.RevenueAccount!;
        article.VatAccount = model.VatAccount;

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
