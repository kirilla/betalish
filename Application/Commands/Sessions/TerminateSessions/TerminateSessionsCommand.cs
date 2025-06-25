using Betalish.Application.Queues.LogItems;

namespace Betalish.Application.Commands.Sessions.TerminateSessions;

public class TerminateSessionsCommand(
    IDateService dateService,
    IDatabaseService database,
    ILogItemList logItemList) : ITerminateSessionsCommand
{
    public async Task Execute(
        IUserToken userToken, TerminateSessionsCommandModel model)
    {
        if (!await IsPermitted(userToken))
            throw new NotPermittedException();

        var sessions = await database.Sessions
            .Where(x => x.Id != userToken.SessionId!.Value)
            .ToListAsync();

        var records = sessions
            .Select(x => new SessionRecord()
            {
                UserId = x.UserId,
                Login = x.Created!.Value,
                Logout = dateService.GetDateTimeNow(),
                IpAddress = x.IpAddress,
                WasForced = true,
            });

        database.SessionRecords.AddRange(records);

        database.Sessions.RemoveRange(sessions);

        logItemList.AddLogItem(new LogItem()
        {
            Description = $"{userToken.Name} terminated all sessions.",
            LogItemKind = LogItemKind.TerminateSessions,
            UserId = userToken.UserId,
        });

        await database.SaveAsync(userToken);
    }

    public async Task<bool> IsPermitted(IUserToken userToken)
    {
        return await database.AdminAuths.AnyAsync(x =>
            x.UserId == userToken.UserId!.Value);
    }
}
