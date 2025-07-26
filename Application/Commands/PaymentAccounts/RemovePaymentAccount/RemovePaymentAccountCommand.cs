namespace Betalish.Application.Commands.PaymentAccounts.RemovePaymentAccount;

public class RemovePaymentAccountCommand(IDatabaseService database) : IRemovePaymentAccountCommand
{
    public async Task Execute(
        IUserToken userToken, RemovePaymentAccountCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var account = await database.PaymentAccounts
            .Where(x => 
                x.Id == model.Id &&
                x.ClientId == userToken.ClientId!.Value)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.PaymentAccounts.Remove(account);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
