namespace Betalish.Application.Commands.EmailAccounts.EditEmailAccount;

public interface IEditEmailAccountCommand
{
    Task Execute(IUserToken userToken, EditEmailAccountCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
