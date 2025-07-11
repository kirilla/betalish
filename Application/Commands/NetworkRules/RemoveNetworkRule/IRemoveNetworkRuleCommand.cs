namespace Betalish.Application.Commands.NetworkRules.RemoveNetworkRule;

public interface IRemoveNetworkRuleCommand
{
    Task Execute(IUserToken userToken, RemoveNetworkRuleCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
