using Betalish.Application.BackgroundServices.Loggers;

namespace Betalish.Tests.Application.BackgroundServices.Loggers;

[TestClass]
public sealed class LogItemLoggerTests
{
    [TestMethod]
    public void Dedup()
    {
        // Arrange
        var description = "Detta har hänt";
        var ipAddress = "255.255.255.255";

        var item1 = new LogItem()
        {
            Description = description,
            IpAddress = ipAddress,
            LogItemKind = LogItemKind.Test,
        };

        var item2 = new LogItem()
        {
            Description = description,
            IpAddress = ipAddress,
            LogItemKind = LogItemKind.Test,
        };

        var item3 = new LogItem()
        {
            Description = description,
            IpAddress = ipAddress,
            LogItemKind = LogItemKind.Test,
        };

        var list = new List<LogItem>() { item1, item2, item3 };

        // Act
        var deduplist = LogItemLogger.Dedup(list);

        // Assert
        Assert.AreEqual(1, deduplist.Count);
    }
}
