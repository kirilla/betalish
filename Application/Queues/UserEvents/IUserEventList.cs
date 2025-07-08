namespace Betalish.Application.Queues.UserEvents;

public interface IUserEventList
{
    void AddUserEvent(UserEvent evt);

    List<UserEvent> TakeUserEvents();
}
