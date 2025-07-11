namespace Betalish.Application.Commands.ClientEvents.RemoveClientEvents;

public interface IRemoveClientEventsCommand
{
    Task Execute(IUserToken userToken, RemoveClientEventsCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
