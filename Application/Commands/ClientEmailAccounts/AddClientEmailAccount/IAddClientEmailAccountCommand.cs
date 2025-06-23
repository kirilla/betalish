namespace Betalish.Application.Commands.ClientEmailAccounts.AddClientEmailAccount;

public interface IAddClientEmailAccountCommand
{
    Task<int> Execute(IUserToken userToken, AddClientEmailAccountCommandModel model);

    Task<bool> IsPermitted(IUserToken userToken);
}
