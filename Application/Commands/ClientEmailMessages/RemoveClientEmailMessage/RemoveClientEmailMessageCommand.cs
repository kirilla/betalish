namespace Betalish.Application.Commands.ClientEmailMessages.RemoveClientEmailMessage;

public class RemoveClientEmailMessageCommand(IDatabaseService database) : IRemoveClientEmailMessageCommand
{
    public async Task Execute(
        IUserToken userToken, 
        RemoveClientEmailMessageCommandModel model)
    {
        if (!await IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var message = await database.ClientEmailMessages
            .Where(x => 
                x.Id == model.Id &&
                x.ClientId == userToken.ClientId!.Value)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.ClientEmailMessages.Remove(message);

        await database.SaveAsync(userToken);
    }

    public async Task<bool> IsPermitted(IUserToken userToken)
    {
        return await database.ClientAuths.AnyAsync(x =>
            x.ClientId == userToken.ClientId!.Value &&
            x.UserId == userToken.UserId!.Value);
    }
}
