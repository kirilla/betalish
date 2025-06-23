namespace Betalish.Application.Commands.ClientEmailMessages.RemoveClientEmailMessage;

public class RemoveClientEmailMessageCommand(IDatabaseService database) : IRemoveClientEmailMessageCommand
{
    public async Task Execute(
        IUserToken userToken, 
        RemoveClientEmailMessageCommandModel model,
        int clientId)
    {
        if (!await IsPermitted(userToken, clientId))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var message = await database.ClientEmailMessages
            .Where(x => 
                x.Id == model.Id &&
                x.ClientId == clientId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.ClientEmailMessages.Remove(message);

        await database.SaveAsync(userToken);
    }

    public async Task<bool> IsPermitted(IUserToken userToken, int clientId)
    {
        return await database.ClientAuths.AnyAsync(x =>
            x.ClientId == clientId &&
            x.UserId == userToken.UserId!.Value);
    }
}
