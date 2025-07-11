namespace Betalish.Application.Commands.NetworkRules.RemoveNetworkRule;

public class RemoveNetworkRuleCommand(
    IDatabaseService database,
    INetworkRuleCacheService cacheService) : IRemoveNetworkRuleCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveNetworkRuleCommandModel model)
    {
        if (!IsPermitted(userToken))
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

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAdmin;
    }
}
