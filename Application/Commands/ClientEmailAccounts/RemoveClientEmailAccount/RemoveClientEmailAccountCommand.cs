namespace Betalish.Application.Commands.ClientEmailAccounts.RemoveClientEmailAccount;

public class RemoveClientEmailAccountCommand(IDatabaseService database) : IRemoveClientEmailAccountCommand
{
    public async Task Execute(
        IUserToken userToken, 
        RemoveClientEmailAccountCommandModel model, 
        int clientId)
    {
        if (!await IsPermitted(userToken, clientId))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var account = await database.ClientEmailAccounts
            .Where(x => x.Id == model.ClientEmailAccountId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.ClientEmailAccounts.Remove(account);

        await database.SaveAsync(userToken);
    }

    public async Task<bool> IsPermitted(IUserToken userToken, int clientId)
    {
        return await database.ClientAuths.AnyAsync(x =>
            x.ClientId == clientId &&
            x.UserId == userToken.UserId!.Value);
    }
}
