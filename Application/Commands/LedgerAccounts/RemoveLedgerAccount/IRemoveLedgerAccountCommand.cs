namespace Betalish.Application.Commands.LedgerAccounts.RemoveLedgerAccount;

public interface IRemoveLedgerAccountCommand
{
    Task Execute(IUserToken userToken, RemoveLedgerAccountCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
