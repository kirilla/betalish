namespace Betalish.Application.Commands.ClientEmailMessages.RemoveClientEmailMessages;

public class RemoveClientEmailMessagesCommand(IDatabaseService database) : IRemoveClientEmailMessagesCommand
{
    public async Task Execute(
        IUserToken userToken, 
        RemoveClientEmailMessagesCommandModel model)
    {
        if (!await IsPermitted(userToken))
            throw new NotPermittedException();

        var query = await database.ClientEmailMessages
            .Where(x => 
                x.ClientId == userToken.ClientId!.Value &&
                x.EmailStatus == model.EmailStatus!.Value)
            .ExecuteDeleteAsync();
    }

    public async Task<bool> IsPermitted(IUserToken userToken)
    {
        return await database.ClientAuths.AnyAsync(x =>
            x.ClientId == userToken.ClientId!.Value &&
            x.UserId == userToken.UserId!.Value);
    }
}
