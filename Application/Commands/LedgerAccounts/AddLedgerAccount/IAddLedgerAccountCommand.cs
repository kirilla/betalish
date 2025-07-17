namespace Betalish.Application.Commands.LedgerAccounts.AddLedgerAccount;

public interface IAddLedgerAccountCommand
{
    Task<int> Execute(IUserToken userToken, AddLedgerAccountCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
