namespace Betalish.Application.Commands.Articles.AddArticle;

public class AddArticleCommand(IDatabaseService database) : IAddArticleCommand
{
    public async Task<int> Execute(
        IUserToken userToken, AddArticleCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        if (await database.Articles
            .AnyAsync(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.Number == model.Number!.Value))
            throw new BlockedByNumberException();

        var article = new Article()
        {
            ArticleKind = model.ArticleKind!.Value,
            Number = model.Number!.Value,
            Name = model.Name!,
            UnitPrice = model.UnitPrice!.TryParseDecimal()!.Value,
            VatValue = model.VatValue!.TryParseDecimal()!.Value,
            UnitName = model.UnitName!.ToLowerInvariant(),
            Account = model.Account!,
            VatAccount = model.VatAccount!,
            ClientId = userToken.ClientId!.Value,
        };

        database.Articles.Add(article);

        await database.SaveAsync(userToken);

        return article.Id;
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
