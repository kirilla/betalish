namespace Betalish.Application.Commands.Signups.FinishSignup;

public interface IFinishSignupCommand
{
    Task Execute(IUserToken userToken, FinishSignupCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
