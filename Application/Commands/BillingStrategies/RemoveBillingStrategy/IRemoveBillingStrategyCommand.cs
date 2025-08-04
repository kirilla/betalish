namespace Betalish.Application.Commands.BillingStrategies.RemoveBillingStrategy;

public interface IRemoveBillingStrategyCommand
{
    Task Execute(IUserToken userToken, RemoveBillingStrategyCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
