namespace Betalish.Application.Commands.NetworkRules.AddNetworkRule;

public interface IAddNetworkRuleCommand
{
    Task Execute(IUserToken userToken, AddNetworkRuleCommandModel model);

    Task<bool> IsPermitted(IUserToken userToken);
}
