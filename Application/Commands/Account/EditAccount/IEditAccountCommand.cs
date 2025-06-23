namespace Betalish.Application.Commands.Account.EditAccount;

public interface IEditAccountCommand
{
    Task Execute(IUserToken userToken, EditAccountCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
