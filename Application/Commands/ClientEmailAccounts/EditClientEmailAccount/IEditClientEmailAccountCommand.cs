namespace Betalish.Application.Commands.ClientEmailAccounts.EditClientEmailAccount;

public interface IEditClientEmailAccountCommand
{
    Task Execute(IUserToken userToken, EditClientEmailAccountCommandModel model, int clientId);

    Task<bool> IsPermitted(IUserToken userToken, int clientId);
}
