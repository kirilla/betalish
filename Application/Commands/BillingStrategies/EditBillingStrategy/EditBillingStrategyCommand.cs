namespace Betalish.Application.Commands.BillingStrategies.EditBillingStrategy;

public class EditBillingStrategyCommand(IDatabaseService database) : IEditBillingStrategyCommand
{
    public async Task Execute(
        IUserToken userToken, EditBillingStrategyCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        if (await database.PaymentTerms
            .AnyAsync(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.Name == model.Name &&
                x.Id != model.Id))
            throw new BlockedByExistingException();

        var minToConsiderPaid = model.MinToConsiderPaid?.TryParseDecimal();

        var strategy = await database.PaymentTerms
            .Where(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        strategy.Name = model.Name!;
        strategy.Interest = model.Interest;
        strategy.Reminder = model.Reminder;
        strategy.Demand = model.Demand;
        strategy.Collect = model.Collect;
        strategy.PaymentTermDays = model.PaymentTermDays!.Value;
        strategy.MinToConsiderPaid = minToConsiderPaid;

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
