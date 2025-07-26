namespace Betalish.Application.Commands.Payments.AddPayment;

public class AddPaymentCommand(IDatabaseService database) : IAddPaymentCommand
{
    public async Task<int> Execute(
        IUserToken userToken, AddPaymentCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var account = new Payment()
        {
            Amount = model.Amount!.TryParseDecimal()!.Value,
            ClientId = userToken.ClientId!.Value,
        };

        database.Payments.Add(account);

        await database.SaveAsync(userToken);

        return account.Id;
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
