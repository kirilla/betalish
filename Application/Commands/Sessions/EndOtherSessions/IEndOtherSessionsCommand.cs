namespace Betalish.Application.Commands.Sessions.EndOtherSessions;

public interface IEndOtherSessionsCommand
{
    Task Execute(IUserToken userToken, EndOtherSessionsCommandModel model);

    Task<bool> IsPermitted(IUserToken userToken);
}
