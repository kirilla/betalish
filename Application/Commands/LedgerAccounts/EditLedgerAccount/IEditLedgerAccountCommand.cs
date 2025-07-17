namespace Betalish.Application.Commands.LedgerAccounts.EditLedgerAccount;

public interface IEditLedgerAccountCommand
{
    Task Execute(IUserToken userToken, EditLedgerAccountCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
