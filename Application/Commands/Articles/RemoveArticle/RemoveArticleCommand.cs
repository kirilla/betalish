namespace Betalish.Application.Commands.Articles.RemoveArticle;

public class RemoveArticleCommand(IDatabaseService database) : IRemoveArticleCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveArticleCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var article = await database.Articles
            .Where(x => 
                x.Id == model.Id &&
                x.ClientId == userToken.ClientId!.Value)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.Articles.Remove(article);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
