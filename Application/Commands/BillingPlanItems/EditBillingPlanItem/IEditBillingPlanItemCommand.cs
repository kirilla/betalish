namespace Betalish.Application.Commands.BillingPlanItems.EditBillingPlanItem;

public interface IEditBillingPlanItemCommand
{
    Task Execute(IUserToken userToken, EditBillingPlanItemCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
