namespace Betalish.Application.Commands.Sessions.TerminateSessions;

public interface ITerminateSessionsCommand
{
    Task Execute(IUserToken userToken, TerminateSessionsCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
