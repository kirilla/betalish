namespace Betalish.Application.Commands.ClientEvents.RemoveClientEvents;

public class RemoveClientEventsCommand(IDatabaseService database) : IRemoveClientEventsCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveClientEventsCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        await database.ClientEvents.ExecuteDeleteAsync();
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAdmin;
    }
}
