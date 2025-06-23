namespace Betalish.Application.Commands.ClientEmailAccounts.AddClientEmailAccount;

public interface IAddClientEmailAccountCommand
{
    Task<int> Execute(IUserToken userToken, AddClientEmailAccountCommandModel model, int clientId);

    Task<bool> IsPermitted(IUserToken userToken, int clientId);
}
