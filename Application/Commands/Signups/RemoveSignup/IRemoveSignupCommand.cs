namespace Betalish.Application.Commands.Signups.RemoveSignup;

public interface IRemoveSignupCommand
{
    Task Execute(IUserToken userToken, RemoveSignupCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
