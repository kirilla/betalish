namespace Betalish.Application.Commands.LedgerAccounts.RemoveLedgerAccount;

public class RemoveLedgerAccountCommand(IDatabaseService database) : IRemoveLedgerAccountCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveLedgerAccountCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var account = await database.LedgerAccounts
            .Where(x => 
                x.Id == model.Id &&
                x.ClientId == userToken.ClientId!.Value)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.LedgerAccounts.Remove(account);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
