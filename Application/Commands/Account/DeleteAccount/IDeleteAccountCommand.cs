namespace Betalish.Application.Commands.Account.DeleteAccount;

public interface IDeleteAccountCommand
{
    Task Execute(IUserToken userToken, DeleteAccountCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
