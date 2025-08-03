namespace Betalish.Application.Commands.BillingPlanItems.AddBillingPlanItem;

public interface IAddBillingPlanItemCommand
{
    Task Execute(IUserToken userToken, AddBillingPlanItemCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
