namespace Betalish.Application.Commands.Payments.AddPayment;

public class AddPaymentCommand(
    IDatabaseService database,
    IDateService dateService) : IAddPaymentCommand
{
    public async Task<int> Execute(
        IUserToken userToken, AddPaymentCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var amount = model.Amount?.TryParseDecimal();

        if (amount == null || amount == 0)
            throw new InvalidAmountException();

        var today = dateService.GetDateOnlyToday();

        var date = model.Date?.ToIso8601DateOnly();

        if (date == null || date > today)
            throw new InvalidDateException();

        var account = await database.PaymentAccounts
            .Where(x =>
                x.Id == model.PaymentAccountId!.Value &&
                x.ClientId == userToken.ClientId!.Value)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        var payment = new Payment()
        {
            Amount = amount!.Value,
            Date = date!.Value,
            
            PaymentKind = PaymentKind.Payment,
            PaymentMethod = model.PaymentMethod!.Value,

            PaymentAccountId = account.Id,
            ClientId = userToken.ClientId!.Value,
        };

        database.Payments.Add(payment);

        await database.SaveAsync(userToken);

        return account.Id;
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
