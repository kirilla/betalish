using System.Net;

namespace Betalish.Application.Commands.NetworkRules.AddNetworkRule;

public class AddNetworkRuleCommand(
    IDatabaseService database,
    INetworkRuleCacheService cacheService) : IAddNetworkRuleCommand
{
    public async Task Execute(
        IUserToken userToken, AddNetworkRuleCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        IPAddress.Parse(model.BaseAddress!);

        var network = $"{model.BaseAddress}/{model.PrefixLength}";

        IPNetwork.Parse(network);

        if (await database.NetworkRules
            .AnyAsync(x => x.BaseAddress == model.BaseAddress))
            throw new BlockedByExistingException();

        var rule = new NetworkRule()
        {
            BaseAddress = model.BaseAddress!,
            PrefixLength = model.PrefixLength!.Value,
            Active = model.Active,
            Block = model.Block,
            Log = model.Log,
            Description = model.Description,
        };

        database.NetworkRules.Add(rule);

        await database.SaveAsync(userToken);

        cacheService.InvalidateCache();
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAdmin;
    }
}
