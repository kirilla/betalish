namespace Betalish.Application.Queues.SessionActivities;

public interface ISessionActivityList
{
    void AddSessionId(int sessionId);

    List<int> RemoveSessionIds();
}
