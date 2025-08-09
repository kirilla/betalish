namespace Betalish.Application.Commands.ClientEmailMessages.RemoveClientEmailMessages;

public class RemoveClientEmailMessagesCommand(IDatabaseService database) : IRemoveClientEmailMessagesCommand
{
    public async Task Execute(
        IUserToken userToken, 
        RemoveClientEmailMessagesCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        var query = await database.MessagesToCustomer
            .Where(x => 
                x.ClientId == userToken.ClientId!.Value &&
                x.EmailStatus == model.EmailStatus!.Value)
            .ExecuteDeleteAsync();
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
