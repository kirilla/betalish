namespace Betalish.Application.Commands.EmailAccounts.RemoveEmailAccount;

public class RemoveEmailAccountCommand(IDatabaseService database) : IRemoveEmailAccountCommand
{
    public async Task Execute(
        IUserToken userToken, 
        RemoveEmailAccountCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var account = await database.EmailAccounts
            .Where(x => x.Id == model.Id)
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
