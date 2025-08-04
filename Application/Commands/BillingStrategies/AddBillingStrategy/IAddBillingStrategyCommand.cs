namespace Betalish.Application.Commands.BillingStrategies.AddBillingStrategy;

public interface IAddBillingStrategyCommand
{
    Task<int> Execute(IUserToken userToken, AddBillingStrategyCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
