namespace Betalish.Application.Commands.PaymentAccounts.AddPaymentAccount;

public class AddPaymentAccountCommand(IDatabaseService database) : IAddPaymentAccountCommand
{
    public async Task<int> Execute(
        IUserToken userToken, AddPaymentAccountCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        if (await database.PaymentAccounts
            .AnyAsync(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.Name == model.Name!))
            throw new BlockedByExistingException();

        var account = new PaymentAccount()
        {
            Name = model.Name!,
            Description = model.Description,
            Account = model.Account!,
            ClientId = userToken.ClientId!.Value,
        };

        database.PaymentAccounts.Add(account);

        await database.SaveAsync(userToken);

        return account.Id;
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
