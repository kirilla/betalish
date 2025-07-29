namespace Betalish.Application.Commands.Payments.EditPayment;

public class EditPaymentCommand(IDatabaseService database) : IEditPaymentCommand
{
    public async Task Execute(
        IUserToken userToken, EditPaymentCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var payment = await database.Payments
            .Where(x =>
                x.Id == model.Id &&
                x.ClientId == userToken.ClientId!.Value)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        if (payment.InvoiceId.HasValue)
            throw new BlockedByInvoiceException();

        var account = await database.PaymentAccounts
            .Where(x =>
                x.Id == model.PaymentAccountId!.Value &&
                x.ClientId == userToken.ClientId!.Value)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        payment.Amount = model.Amount!.TryParseDecimal()!.Value;
        payment.Date = model.Date!.ToIso8601DateOnly()!.Value;
        payment.PaymentMethod = model.PaymentMethod!.Value;
        payment.PaymentAccountId = account.Id;

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
