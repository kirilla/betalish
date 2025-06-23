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

        if (!model.Range.Contains("/"))
            throw new MissingPartException();

        if (model.Range.Split('/').Length != 2)
            throw new MissingPartException();

        IPNetwork.Parse(model.Range);

        var range = await database.NetworkRules
            .Where(x => x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        if (await database.NetworkRules
            .AnyAsync(x => 
                x.Range == model.Range &&
                x.Id != model.Id))
            throw new BlockedByExistingException();

        range.Range = model.Range;
        range.Blocked = model.Blocked;

        await database.SaveAsync(userToken);

        cacheService.InvalidateCache();
    }

    public async Task<bool> IsPermitted(IUserToken userToken)
    {
        return await database.AdminAuths.AnyAsync(x =>
            x.UserId == userToken.UserId!.Value);
    }
}
