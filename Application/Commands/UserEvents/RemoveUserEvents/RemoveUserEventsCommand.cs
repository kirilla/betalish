namespace Betalish.Application.Commands.UserEvents.RemoveUserEvents;

public class RemoveUserEventsCommand(IDatabaseService database) : IRemoveUserEventsCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveUserEventsCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        await database.UserEvents.ExecuteDeleteAsync();
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAdmin;
    }
}
