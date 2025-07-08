namespace Betalish.Application.Commands.NetworkRules.RemoveNetworkRule;

public class RemoveNetworkRuleCommand(
    IDatabaseService database,
    INetworkRuleCacheService cacheService) : IRemoveNetworkRuleCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveNetworkRuleCommandModel model)
    {
        if (!await IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var rule = await database.NetworkRules
            .Where(x => x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.NetworkRules.Remove(rule);

        await database.SaveAsync(userToken);

        cacheService.InvalidateCache();
    }

    public async Task<bool> IsPermitted(IUserToken userToken)
    {
        return await database.AdminAuths.AnyAsync(x =>
            x.UserId == userToken.UserId!.Value);
    }
}
