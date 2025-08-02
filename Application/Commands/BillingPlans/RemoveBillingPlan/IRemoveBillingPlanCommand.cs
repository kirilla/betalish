namespace Betalish.Application.Commands.BillingPlans.RemoveBillingPlan;

public interface IRemoveBillingPlanCommand
{
    Task Execute(IUserToken userToken, RemoveBillingPlanCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
