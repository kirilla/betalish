namespace Betalish.Application.Commands.ClientEmailAccounts.RemoveClientEmailAccount;

public interface IRemoveClientEmailAccountCommand
{
    Task Execute(IUserToken userToken, RemoveClientEmailAccountCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
