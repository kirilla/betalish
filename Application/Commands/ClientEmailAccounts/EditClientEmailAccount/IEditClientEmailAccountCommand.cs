namespace Betalish.Application.Commands.ClientEmailAccounts.EditClientEmailAccount;

public interface IEditClientEmailAccountCommand
{
    Task Execute(IUserToken userToken, EditClientEmailAccountCommandModel model);

    Task<bool> IsPermitted(IUserToken userToken);
}
