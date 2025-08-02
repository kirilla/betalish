namespace Betalish.Application.Commands.BillingPlans.EditBillingPlan;

public class EditBillingPlanCommand(IDatabaseService database) : IEditBillingPlanCommand
{
    public async Task Execute(
        IUserToken userToken, EditBillingPlanCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        if (await database.BillingPlans
            .AnyAsync(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.Name == model.Name &&
                x.Id != model.Id))
            throw new BlockedByExistingException();

        var plan = await database.BillingPlans
            .Where(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        plan.Name = model.Name!;

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
