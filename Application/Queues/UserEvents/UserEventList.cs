using Microsoft.Extensions.Hosting;

namespace Betalish.Application.Queues.UserEvents;

public class UserEventList(
    IHostEnvironment hostEnvironment,
    IDateService dateService) : IUserEventList
{
    private List<UserEvent> list { get; } = [];

    public void AddUserEvent(UserEvent evt)
    {
        if (!hostEnvironment.IsProduction())
            return;

        lock (this)
        {
            evt.Created = dateService.GetDateTimeNow();

            list.Add(evt);
        }
    }

    public List<UserEvent> TakeUserEvents()
    {
        lock (this)
        {
            var entries = list.ToList();

            list.Clear();

            return entries;
        }
    }
}
