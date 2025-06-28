namespace Betalish.Application.Commands.Signups.SignupForService;

public interface ISignupForServiceCommand
{
    Task Execute(IUserToken userToken, SignupForServiceCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
