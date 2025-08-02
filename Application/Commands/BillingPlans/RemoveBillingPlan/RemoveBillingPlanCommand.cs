namespace Betalish.Application.Commands.BillingPlans.RemoveBillingPlan;

public class RemoveBillingPlanCommand(IDatabaseService database) : IRemoveBillingPlanCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveBillingPlanCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var plan = await database.BillingPlans
            .Where(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.BillingPlans.Remove(plan);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
