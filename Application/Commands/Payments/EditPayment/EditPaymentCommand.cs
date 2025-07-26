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

        var account = await database.Payments
            .Where(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        account.Amount = model.Amount!.TryParseDecimal()!.Value;

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
