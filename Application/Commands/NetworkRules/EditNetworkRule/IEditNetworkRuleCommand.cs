namespace Betalish.Application.Commands.NetworkRules.EditNetworkRule;

public interface IEditNetworkRuleCommand
{
    Task Execute(IUserToken userToken, EditNetworkRuleCommandModel model);

    Task<bool> IsPermitted(IUserToken userToken);
}
