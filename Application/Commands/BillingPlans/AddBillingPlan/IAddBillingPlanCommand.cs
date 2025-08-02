namespace Betalish.Application.Commands.BillingPlans.AddBillingPlan;

public interface IAddBillingPlanCommand
{
    Task<int> Execute(IUserToken userToken, AddBillingPlanCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
