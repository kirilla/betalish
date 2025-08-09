namespace Betalish.Application.Commands.ClientEmailAccounts.RemoveClientEmailAccount;

public class RemoveClientEmailAccountCommand(IDatabaseService database) : IRemoveClientEmailAccountCommand
{
    public async Task Execute(
        IUserToken userToken, 
        RemoveClientEmailAccountCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var account = await database.EmailAccounts
            .Where(x => x.Id == model.ClientEmailAccountId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.EmailAccounts.Remove(account);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
