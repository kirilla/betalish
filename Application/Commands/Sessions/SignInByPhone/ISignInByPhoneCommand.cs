namespace Betalish.Application.Commands.Sessions.SignInByPhone;

public interface ISignInByPhoneCommand
{
    Task<SessionGuidResultModel> Execute(
        IUserToken userToken, SignInByPhoneCommandModel model, string? ipAddress);

    bool IsPermitted(IUserToken userToken);
}
