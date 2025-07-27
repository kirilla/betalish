namespace Betalish.Application.Commands.Payments.UnassignPayment;

public class UnassignPaymentCommand(IDatabaseService database) : IUnassignPaymentCommand
{
    public async Task Execute(
        IUserToken userToken, UnassignPaymentCommandModel model)
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

        if (payment.InvoiceId == null)
            throw new NotAssignedException();
        
        // TODO: More asserts

        // If debit, if credit, ...?

        payment.InvoiceId = null;
        payment.InvoiceNumber = null;

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
