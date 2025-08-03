namespace Betalish.Application.Commands.BillingPlanItems.RemoveBillingPlanItem;

public class RemoveBillingPlanItemCommand(IDatabaseService database) : IRemoveBillingPlanItemCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveBillingPlanItemCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var item = await database.BillingPlanItems
            .Where(x =>
                x.BillingPlan.ClientId == userToken.ClientId!.Value &&
                x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        var planId = item.BillingPlanId;

        database.BillingPlanItems.Remove(item);
        
        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
