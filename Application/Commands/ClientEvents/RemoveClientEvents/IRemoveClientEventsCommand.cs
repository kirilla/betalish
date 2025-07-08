namespace Betalish.Application.Commands.ClientEvents.RemoveClientEvents;

public interface IRemoveClientEventsCommand
{
    Task Execute(IUserToken userToken, RemoveClientEventsCommandModel model);

    Task<bool> IsPermitted(IUserToken userToken);
}
