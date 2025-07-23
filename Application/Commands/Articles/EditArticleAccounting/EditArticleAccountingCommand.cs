namespace Betalish.Application.Commands.Articles.EditArticleAccounting;

public class EditArticleAccountingCommand(IDatabaseService database) : IEditArticleAccountingCommand
{
    public async Task Execute(
        IUserToken userToken, EditArticleAccountingCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var article = await database.Articles
            .Where(x =>
                x.Id == model.Id &&
                x.ClientId == userToken.ClientId!.Value)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        article.RevenueAccount = model.RevenueAccount!;
        article.VatAccount = model.VatAccount;

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
