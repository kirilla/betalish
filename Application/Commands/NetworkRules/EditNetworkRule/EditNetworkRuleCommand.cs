using System.Net;

namespace Betalish.Application.Commands.NetworkRules.EditNetworkRule;

public class EditNetworkRuleCommand(
    IDatabaseService database,
    INetworkRuleCacheService cacheService) : IEditNetworkRuleCommand
{
    public async Task Execute(
        IUserToken userToken, EditNetworkRuleCommandModel model)
    {
        if (!await IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        IPAddress.Parse(model.BaseAddress2);

        var network = $"{model.BaseAddress2}/{model.Prefix2}";

        IPNetwork.Parse(network);

        var rule = await database.NetworkRules
            .Where(x => x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        if (await database.NetworkRules
            .AnyAsync(x => 
                x.BaseAddress2 == model.BaseAddress2 &&
                x.Id != model.Id))
            throw new BlockedByExistingException();

        rule.BaseAddress2 = model.BaseAddress2;
        rule.Prefix2 = model.Prefix2;
        rule.Blocked = model.Blocked;

        await database.SaveAsync(userToken);

        cacheService.InvalidateCache();
    }

    public async Task<bool> IsPermitted(IUserToken userToken)
    {
        return await database.AdminAuths.AnyAsync(x =>
            x.UserId == userToken.UserId!.Value);
    }
}
