namespace Betalish.Application.Queues.LogItems;

public interface ILogItemList
{
    void AddLogItem(LogItem logItem);

    List<LogItem> TakeLogItems();
}
