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

        if (!model.Range.Contains("/"))
            throw new MissingPartException();

        if (model.Range.Split('/').Length != 2)
            throw new MissingPartException();

        IPNetwork.Parse(model.Range);

        if (await database.NetworkRules
            .AnyAsync(x => x.Range == model.Range))
            throw new BlockedByExistingException();

        var range = new NetworkRule()
        {
            Range = model.Range,
            BaseAddress2 = model.BaseAddress2,
            Prefix2 = model.Prefix2,
            Blocked = model.Blocked,
        };

        database.NetworkRules.Add(range);

        await database.SaveAsync(userToken);

        cacheService.InvalidateCache();
    }

    public async Task<bool> IsPermitted(IUserToken userToken)
    {
        return await database.AdminAuths.AnyAsync(x =>
            x.UserId == userToken.UserId!.Value);
    }
}
