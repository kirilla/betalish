namespace Betalish.Application.Queues.LogItems;

public class LogItemList(IDateService dateService) : ILogItemList
{
    private List<LogItem> list { get; set; } = new List<LogItem>();

    public void AddLogItem(LogItem logItem)
    {
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
