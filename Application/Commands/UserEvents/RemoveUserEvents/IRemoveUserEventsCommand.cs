namespace Betalish.Application.Commands.UserEvents.RemoveUserEvents;

public interface IRemoveUserEventsCommand
{
    Task Execute(IUserToken userToken, RemoveUserEventsCommandModel model);

    Task<bool> IsPermitted(IUserToken userToken);
}
