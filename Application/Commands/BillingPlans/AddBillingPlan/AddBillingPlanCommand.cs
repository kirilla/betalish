namespace Betalish.Application.Commands.BillingPlans.AddBillingPlan;

public class AddBillingPlanCommand(IDatabaseService database) : IAddBillingPlanCommand
{
    public async Task<int> Execute(
        IUserToken userToken, AddBillingPlanCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        if (await database.BillingPlans
            .AnyAsync(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.Name == model.Name))
            throw new BlockedByExistingException();

        var plan = new BillingPlan()
        {
            Name = model.Name!,
            ClientId = userToken.ClientId!.Value,
        };

        database.BillingPlans.Add(plan);

        await database.SaveAsync(userToken);

        return plan.Id;
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
