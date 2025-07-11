namespace Betalish.Application.Commands.NetworkRules.EditNetworkRule;

public interface IEditNetworkRuleCommand
{
    Task Execute(IUserToken userToken, EditNetworkRuleCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
