namespace Betalish.Application.Commands.Articles.RemoveArticle;

public interface IRemoveArticleCommand
{
    Task Execute(IUserToken userToken, RemoveArticleCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
