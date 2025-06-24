using Microsoft.Extensions.Hosting;

namespace Betalish.Application.Queues.LogItems;

public class LogItemList(
    IHostEnvironment hostEnvironment,
    IDateService dateService) : ILogItemList
{
    private List<LogItem> list { get; set; } = new List<LogItem>();

    public void AddLogItem(LogItem logItem)
    {
        if (!hostEnvironment.IsProduction())
            return;

        lock (this)
        {
            logItem.Created = dateService.GetDateTimeNow();

            list.Add(logItem);
        }
    }

    public List<LogItem> TakeLogItems()
    {
        lock (this)
        {
            var entries = list.ToList();

            list.Clear();

            return entries;
        }
    }
}
