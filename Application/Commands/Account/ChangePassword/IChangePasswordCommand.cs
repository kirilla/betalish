namespace Betalish.Application.Commands.Account.ChangePassword;

public interface IChangePasswordCommand
{
    Task Execute(IUserToken userToken, ChangePasswordCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
