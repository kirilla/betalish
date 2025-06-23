namespace Betalish.Application.Commands.ClientEmailMessages.RemoveClientEmailMessages;

public class RemoveClientEmailMessagesCommand(IDatabaseService database) : IRemoveClientEmailMessagesCommand
{
    public async Task Execute(
        IUserToken userToken, 
        RemoveClientEmailMessagesCommandModel model, 
        int clientId)
    {
        if (!await IsPermitted(userToken, clientId))
            throw new NotPermittedException();

        var query = await database.ClientEmailMessages
            .Where(x => 
                x.ClientId == clientId &&
                x.EmailStatus == model.EmailStatus!.Value)
            .ExecuteDeleteAsync();
    }

    public async Task<bool> IsPermitted(IUserToken userToken, int clientId)
    {
        return await database.ClientAuths.AnyAsync(x =>
            x.ClientId == clientId &&
            x.UserId == userToken.UserId!.Value);
    }
}
