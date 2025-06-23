namespace Betalish.Application.Commands.Clients.RemoveClient;

public class RemoveClientCommand(IDatabaseService database) : IRemoveClientCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveClientCommandModel model)
    {
        if (!await IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var client = await database.Clients
            .Where(x => x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.Clients.Remove(client);

        await database.SaveAsync(userToken);
    }

    public async Task<bool> IsPermitted(IUserToken userToken)
    {
        return await database.AdminAuths.AnyAsync(x =>
            x.UserId == userToken.UserId!.Value);
    }
}
