namespace Betalish.Application.Commands.ClientEmailAccounts.AddClientEmailAccount;

public interface IAddClientEmailAccountCommand
{
    Task<int> Execute(IUserToken userToken, AddClientEmailAccountCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
