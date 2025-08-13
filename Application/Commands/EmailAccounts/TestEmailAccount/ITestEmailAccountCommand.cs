namespace Betalish.Application.Commands.EmailAccounts.TestEmailAccount;

public interface ITestEmailAccountCommand
{
    Task Execute(IUserToken userToken, TestEmailAccountCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
