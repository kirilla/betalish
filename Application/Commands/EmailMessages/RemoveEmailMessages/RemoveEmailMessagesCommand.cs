namespace Betalish.Application.Commands.EmailMessages.RemoveEmailMessages;

public class RemoveEmailMessagesCommand(IDatabaseService database) : IRemoveEmailMessagesCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveEmailMessagesCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        var query = await database.EmailMessages
            .Where(x => x.EmailStatus == model.EmailStatus!.Value)
            .ExecuteDeleteAsync();
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAdmin;
    }
}
