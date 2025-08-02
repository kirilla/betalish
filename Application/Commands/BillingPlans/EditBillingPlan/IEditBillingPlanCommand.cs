namespace Betalish.Application.Commands.BillingPlans.EditBillingPlan;

public interface IEditBillingPlanCommand
{
    Task Execute(IUserToken userToken, EditBillingPlanCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
