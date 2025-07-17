namespace Betalish.Application.Commands.LedgerAccounts.AddLedgerAccount;

public class AddLedgerAccountCommand(IDatabaseService database) : IAddLedgerAccountCommand
{
    public async Task<int> Execute(
        IUserToken userToken, AddLedgerAccountCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        if (await database.LedgerAccounts
            .AnyAsync(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.Account == model.Account!))
            throw new BlockedByExistingException();

        var acccount = new LedgerAccount()
        {
            Account = model.Account!,
            Description = model.Description!,
            ClientId = userToken.ClientId!.Value,
        };

        database.LedgerAccounts.Add(acccount);

        await database.SaveAsync(userToken);

        return acccount.Id;
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
