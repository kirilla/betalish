namespace Betalish.Application.Commands.Sessions.EndOtherSessions;

public class EndOtherSessionsCommand(
    IDateService dateService,
    IDatabaseService database) : IEndOtherSessionsCommand
{
    public async Task Execute(
        IUserToken userToken, EndOtherSessionsCommandModel model)
    {
        if (!await IsPermitted(userToken))
            throw new NotPermittedException();

        var sessions = await database.Sessions
            .Where(x => x.Id != userToken.SessionId!.Value)
            .ToListAsync();

        database.Sessions.RemoveRange(sessions);

        await database.SaveAsync(userToken);
    }

    public async Task<bool> IsPermitted(IUserToken userToken)
    {
        return await database.AdminAuths.AnyAsync(x =>
            x.UserId == userToken.UserId!.Value);
    }
}
