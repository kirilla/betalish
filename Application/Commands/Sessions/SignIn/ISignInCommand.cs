namespace Betalish.Application.Commands.Sessions.SignIn;

public interface ISignInCommand
{
    Task<SessionGuidResultModel> Execute(
        IUserToken userToken, SignInCommandModel model, string? ipAddress);

    bool IsPermitted(IUserToken userToken);
}
