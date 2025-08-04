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

        if (await database.BillingStrategies
            .AnyAsync(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.Name == model.Name))
            throw new BlockedByExistingException();

        var strategy = new BillingStrategy()
        {
            Name = model.Name!,
            ClientId = userToken.ClientId!.Value,
        };

        database.BillingStrategies.Add(strategy);

        await database.SaveAsync(userToken);

        return strategy.Id;
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
