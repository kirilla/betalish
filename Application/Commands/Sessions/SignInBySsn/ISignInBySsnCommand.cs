namespace Betalish.Application.Commands.Sessions.SignInBySsn;

public interface ISignInBySsnCommand
{
    Task<SessionGuidResultModel> Execute(
        IUserToken userToken, SignInBySsnCommandModel model, string? ipAddress);

    bool IsPermitted(IUserToken userToken);
}
