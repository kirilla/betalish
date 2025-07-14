namespace Betalish.Application.Commands.Articles.EditArticle;

public interface IEditArticleCommand
{
    Task Execute(IUserToken userToken, EditArticleCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
