namespace Betalish.Application.Commands.PaymentAccounts.EditPaymentAccount;

public class EditPaymentAccountCommand(IDatabaseService database) : IEditPaymentAccountCommand
{
    public async Task Execute(
        IUserToken userToken, EditPaymentAccountCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var account = await database.PaymentAccounts
            .Where(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        if (await database.PaymentAccounts
            .AnyAsync(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.Name == model.Name! &&
                x.Id != model.Id))
            throw new BlockedByExistingException();

        account.Name = model.Name!;
        account.Description = model.Description;
        account.Account = model.Account!;

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
