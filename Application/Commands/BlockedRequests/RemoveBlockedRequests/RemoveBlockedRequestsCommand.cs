namespace Betalish.Application.Commands.BlockedRequests.RemoveBlockedRequests;

public class RemoveBlockedRequestsCommand(IDatabaseService database) : IRemoveBlockedRequestsCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveBlockedRequestsCommandModel model)
    {
        if (!await IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        await database.BlockedRequests.ExecuteDeleteAsync();
    }

    public async Task<bool> IsPermitted(IUserToken userToken)
    {
        return await database.AdminAuths.AnyAsync(x =>
            x.UserId == userToken.UserId!.Value);
    }
}
