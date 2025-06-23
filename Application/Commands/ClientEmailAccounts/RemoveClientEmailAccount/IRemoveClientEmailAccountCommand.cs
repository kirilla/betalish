namespace Betalish.Application.Commands.ClientEmailAccounts.RemoveClientEmailAccount;

public interface IRemoveClientEmailAccountCommand
{
    Task Execute(IUserToken userToken, RemoveClientEmailAccountCommandModel model);

    Task<bool> IsPermitted(IUserToken userToken);
}
