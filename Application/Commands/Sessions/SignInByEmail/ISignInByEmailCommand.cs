namespace Betalish.Application.Commands.Sessions.SignInByEmail;

public interface ISignInByEmailCommand
{
    Task<SessionGuidResultModel> Execute(
        IUserToken userToken, SignInByEmailCommandModel model, string? ipAddress);

    bool IsPermitted(IUserToken userToken);
}
