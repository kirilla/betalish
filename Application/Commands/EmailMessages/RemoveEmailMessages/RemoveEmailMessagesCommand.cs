namespace Betalish.Application.Commands.EmailMessages.RemoveEmailMessages;

public class RemoveEmailMessagesCommand(IDatabaseService database) : IRemoveEmailMessagesCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveEmailMessagesCommandModel model)
    {
        if (!await IsPermitted(userToken))
            throw new NotPermittedException();

        var query = await database.EmailMessages
            .Where(x => x.EmailStatus == model.EmailStatus!.Value)
            .ExecuteDeleteAsync();
    }

    public async Task<bool> IsPermitted(IUserToken userToken)
    {
        return await database.AdminAuths.AnyAsync(x =>
            x.UserId == userToken.UserId!.Value);
    }
}
