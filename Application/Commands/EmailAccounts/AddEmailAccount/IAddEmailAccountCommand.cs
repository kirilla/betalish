namespace Betalish.Application.Commands.EmailAccounts.AddEmailAccount;

public interface IAddEmailAccountCommand
{
    Task<int> Execute(IUserToken userToken, AddEmailAccountCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
