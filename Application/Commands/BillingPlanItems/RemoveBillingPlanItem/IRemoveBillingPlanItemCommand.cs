namespace Betalish.Application.Commands.BillingPlanItems.RemoveBillingPlanItem;

public interface IRemoveBillingPlanItemCommand
{
    Task Execute(IUserToken userToken, RemoveBillingPlanItemCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
