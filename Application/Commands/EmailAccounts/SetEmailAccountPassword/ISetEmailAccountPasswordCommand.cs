namespace Betalish.Application.Commands.EmailAccounts.SetEmailAccountPassword;

public interface ISetEmailAccountPasswordCommand
{
    Task Execute(IUserToken userToken, SetEmailAccountPasswordCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
