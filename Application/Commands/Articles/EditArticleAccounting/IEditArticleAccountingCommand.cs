namespace Betalish.Application.Commands.Articles.EditArticleAccounting;

public interface IEditArticleAccountingCommand
{
    Task Execute(IUserToken userToken, EditArticleAccountingCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
