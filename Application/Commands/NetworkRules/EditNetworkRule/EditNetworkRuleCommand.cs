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

        IPAddress.Parse(model.BaseAddress);

        var network = $"{model.BaseAddress}/{model.PrefixLength}";

        IPNetwork.Parse(network);

        var rule = await database.NetworkRules
            .Where(x => x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        if (await database.NetworkRules
            .AnyAsync(x => 
                x.BaseAddress == model.BaseAddress &&
                x.Id != model.Id))
            throw new BlockedByExistingException();

        rule.BaseAddress = model.BaseAddress;
        rule.PrefixLength = model.PrefixLength!.Value;
        rule.Blocked = model.Blocked;
        rule.Log = model.Log;

        await database.SaveAsync(userToken);

        cacheService.InvalidateCache();
    }

    public async Task<bool> IsPermitted(IUserToken userToken)
    {
        return await database.AdminAuths.AnyAsync(x =>
            x.UserId == userToken.UserId!.Value);
    }
}
