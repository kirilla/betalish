namespace Betalish.Application.Commands.Sessions.TerminateSessions;

public interface ITerminateSessionsCommand
{
    Task Execute(IUserToken userToken, TerminateSessionsCommandModel model);

    Task<bool> IsPermitted(IUserToken userToken);
}
