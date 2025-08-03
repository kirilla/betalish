namespace Betalish.Application.Commands.BillingPlanItems.EditBillingPlanItem;

public class EditBillingPlanItemCommand(IDatabaseService database) : IEditBillingPlanItemCommand
{
    public async Task Execute(
        IUserToken userToken, EditBillingPlanItemCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var item = await database.BillingPlanItems
            .Where(x =>
                x.BillingPlan.ClientId == userToken.ClientId!.Value &&
                x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        item.OnDay = model.OnDay;
        
        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
