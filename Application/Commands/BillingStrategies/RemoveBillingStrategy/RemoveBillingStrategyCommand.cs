namespace Betalish.Application.Commands.BillingStrategies.RemoveBillingStrategy;

public class RemoveBillingStrategyCommand(IDatabaseService database) : IRemoveBillingStrategyCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveBillingStrategyCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var strategy = await database.PaymentTerms
            .Where(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.PaymentTerms.Remove(strategy);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
