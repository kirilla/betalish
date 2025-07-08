namespace Betalish.Application.Commands.ClientEvents.RemoveClientEvents;

public class RemoveClientEventsCommand(IDatabaseService database) : IRemoveClientEventsCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveClientEventsCommandModel model)
    {
        if (!await IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        await database.ClientEvents.ExecuteDeleteAsync();
    }

    public async Task<bool> IsPermitted(IUserToken userToken)
    {
        return await database.AdminAuths.AnyAsync(x =>
            x.UserId == userToken.UserId!.Value);
    }
}
