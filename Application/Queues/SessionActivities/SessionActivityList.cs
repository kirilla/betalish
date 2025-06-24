using Microsoft.Extensions.Hosting;

namespace Betalish.Application.Queues.SessionActivities;

public class SessionActivityList(
    IHostEnvironment hostEnvironment) : ISessionActivityList
{
    private List<int> SessionIdList { get; } = new List<int>();

    public void AddSessionId(int sessionId)
    {
        if (!hostEnvironment.IsProduction())
            return;

        lock (this)
        {
            SessionIdList.Add(sessionId);
        }
    }

    public List<int> RemoveSessionIds()
    {
        lock (this)
        {
            var entries = SessionIdList
                .Distinct()
                .ToList();

            SessionIdList.Clear();

            return entries;
        }
    }
}
