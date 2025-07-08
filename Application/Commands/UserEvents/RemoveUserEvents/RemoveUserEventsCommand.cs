namespace Betalish.Application.Commands.UserEvents.RemoveUserEvents;

public class RemoveUserEventsCommand(IDatabaseService database) : IRemoveUserEventsCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveUserEventsCommandModel model)
    {
        if (!await IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        await database.UserEvents.ExecuteDeleteAsync();
    }

    public async Task<bool> IsPermitted(IUserToken userToken)
    {
        return await database.AdminAuths.AnyAsync(x =>
            x.UserId == userToken.UserId!.Value);
    }
}
