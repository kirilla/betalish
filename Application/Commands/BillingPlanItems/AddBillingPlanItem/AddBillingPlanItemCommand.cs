namespace Betalish.Application.Commands.BillingPlanItems.AddBillingPlanItem;

public class AddBillingPlanItemCommand(IDatabaseService database) : IAddBillingPlanItemCommand
{
    public async Task Execute(
        IUserToken userToken, AddBillingPlanItemCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var plan = await database.BillingPlans
            .Where(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.Id == model.BillingPlanId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        var item = new BillingPlanItem()
        {
            BillingPlanId = plan.Id,

            PlannedItemKind = model.PlannedItemKind!.Value,
            OnDay = model.OnDay,
        };

        database.BillingPlanItems.Add(item);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
