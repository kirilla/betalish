namespace Betalish.Application.Commands.EmailAccounts.RemoveEmailAccount;

public interface IRemoveEmailAccountCommand
{
    Task Execute(IUserToken userToken, RemoveEmailAccountCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
