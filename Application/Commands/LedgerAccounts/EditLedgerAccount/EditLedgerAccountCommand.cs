namespace Betalish.Application.Commands.LedgerAccounts.EditLedgerAccount;

public class EditLedgerAccountCommand(IDatabaseService database) : IEditLedgerAccountCommand
{
    public async Task Execute(
        IUserToken userToken, EditLedgerAccountCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var account = await database.LedgerAccounts
            .Where(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        if (await database.LedgerAccounts
            .AnyAsync(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.Account == model.Account! &&
                x.Id != model.Id))
            throw new BlockedByExistingException();

        account.Account = model.Account!;
        account.Description = model.Description!;

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
