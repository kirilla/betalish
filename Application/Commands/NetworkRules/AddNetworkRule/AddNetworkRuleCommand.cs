using System.Net;

namespace Betalish.Application.Commands.NetworkRules.AddNetworkRule;

public class AddNetworkRuleCommand(
    IDatabaseService database,
    INetworkRuleCacheService cacheService) : IAddNetworkRuleCommand
{
    public async Task Execute(
        IUserToken userToken, AddNetworkRuleCommandModel model)
    {
        if (!await IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        IPAddress.Parse(model.BaseAddress2);

        var network = $"{model.BaseAddress2}/{model.Prefix2}";

        IPNetwork.Parse(network);

        if (await database.NetworkRules
            .AnyAsync(x => x.BaseAddress2 == model.BaseAddress2))
            throw new BlockedByExistingException();

        var rule = new NetworkRule()
        {
            BaseAddress2 = model.BaseAddress2,
            Prefix2 = model.Prefix2,
            Blocked = model.Blocked,
        };

        database.NetworkRules.Add(rule);

        await database.SaveAsync(userToken);

        cacheService.InvalidateCache();
    }

    public async Task<bool> IsPermitted(IUserToken userToken)
    {
        return await database.AdminAuths.AnyAsync(x =>
            x.UserId == userToken.UserId!.Value);
    }
}
