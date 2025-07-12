using Betalish.Application.BackgroundServices.Loggers;

namespace Betalish.Application.Commands.Tests.TestLogItemDedup;

public class TestLogItemDedupCommand() : ITestLogItemDedupCommand
{
    public async Task Execute(
        IUserToken userToken, TestLogItemDedupCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var item1 = new LogItem()
        {
            Description = "Detta har hänt",
            IpAddress = "255.255.255.255",
            LogItemKind = LogItemKind.Test,
        };

        var item2 = new LogItem()
        {
            Description = "Detta har hänt",
            IpAddress = "255.255.255.255",
            LogItemKind = LogItemKind.Test,
        };

        var item3 = new LogItem()
        {
            Description = "Detta har hänt",
            IpAddress = "255.255.255.255",
            LogItemKind = LogItemKind.Test,
        };

        var list = new List<LogItem>() { item1, item2, item3 };

        var deduplist = LogItemLogger.Dedup(list);

        if (deduplist.Count != 1)
            throw new Exception(
                $"Dedup list count expected 1, got {deduplist.Count}.");
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAdmin;
    }
}
