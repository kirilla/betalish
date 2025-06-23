namespace Betalish.Application.Commands.ClientEmailAccounts.RemoveClientEmailAccount;

public interface IRemoveClientEmailAccountCommand
{
    Task Execute(IUserToken userToken, RemoveClientEmailAccountCommandModel model, int clientId);

    Task<bool> IsPermitted(IUserToken userToken, int clientId);
}
