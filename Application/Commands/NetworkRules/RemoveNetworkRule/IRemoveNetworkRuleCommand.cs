namespace Betalish.Application.Commands.NetworkRules.RemoveNetworkRule;

public interface IRemoveNetworkRuleCommand
{
    Task Execute(IUserToken userToken, RemoveNetworkRuleCommandModel model);

    Task<bool> IsPermitted(IUserToken userToken);
}
