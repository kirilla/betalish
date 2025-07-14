namespace Betalish.Application.Commands.Articles.AddArticle;

public interface IAddArticleCommand
{
    Task<int> Execute(IUserToken userToken, AddArticleCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
