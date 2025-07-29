using Microsoft.Extensions.Hosting;

namespace Betalish.Application.Queues.LogItems;

public class LogItemList(
    IHostEnvironment hostEnvironment,
    IDateService dateService) : ILogItemList
{
    private List<LogItem> List { get; } = [];

    public void AddLogItem(LogItem logItem)
    {
        if (!hostEnvironment.IsProduction())
            return;

        lock (this)
        {
            logItem.Created = dateService.GetDateTimeNow();

            List.Add(logItem);
        }
    }

    public List<LogItem> TakeLogItems()
    {
        lock (this)
        {
            var entries = List.ToList();

            List.Clear();

            return entries;
        }
    }
}
