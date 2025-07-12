namespace Betalish.Application.Commands.ClientEmailMessages.RemoveClientEmailMessage;

public class RemoveClientEmailMessageCommand(IDatabaseService database) : IRemoveClientEmailMessageCommand
{
    public async Task Execute(
        IUserToken userToken, 
        RemoveClientEmailMessageCommandModel model)
    {
        if (!IsPermitted(userToken))
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

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
