namespace Betalish.Application.Commands.BillingStrategies.AddBillingStrategy;

public class AddBillingStrategyCommand(IDatabaseService database) : IAddBillingStrategyCommand
{
    public async Task<int> Execute(
        IUserToken userToken, AddBillingStrategyCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        if (await database.PaymentTerms
            .AnyAsync(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.Name == model.Name))
            throw new BlockedByExistingException();

        var minToConsiderPaid = model.MinToConsiderPaid?.TryParseDecimal();

        var strategy = new PaymentTerms()
        {
            Name = model.Name!,
            ClientId = userToken.ClientId!.Value,
            Interest = model.Interest,
            Reminder = model.Reminder,
            Demand = model.Demand,
            Collect = model.Collect,
            PaymentTermDays = model.PaymentTermDays!.Value,
            MinToConsiderPaid = minToConsiderPaid,
        };

        database.PaymentTerms.Add(strategy);

        await database.SaveAsync(userToken);

        return strategy.Id;
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
