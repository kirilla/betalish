namespace Betalish.Application.Commands.Payments.RemovePayment;

public class RemovePaymentCommand(IDatabaseService database) : IRemovePaymentCommand
{
    public async Task Execute(
        IUserToken userToken, RemovePaymentCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var payment = await database.Payments
            .Where(x => 
                x.Id == model.Id &&
                x.ClientId == userToken.ClientId!.Value)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.Payments.Remove(payment);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
