namespace Betalish.Application.Commands.BillingStrategies.EditBillingStrategy;

public interface IEditBillingStrategyCommand
{
    Task Execute(IUserToken userToken, EditBillingStrategyCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
