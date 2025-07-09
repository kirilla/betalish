namespace Betalish.Application.Commands.NetworkRequests.RemoveNetworkRequests;

public class RemoveNetworkRequestsCommand(IDatabaseService database) : IRemoveNetworkRequestsCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveNetworkRequestsCommandModel model)
    {
        if (!await IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        await database.NetworkRequests.ExecuteDeleteAsync();
    }

    public async Task<bool> IsPermitted(IUserToken userToken)
    {
        return await database.AdminAuths.AnyAsync(x =>
            x.UserId == userToken.UserId!.Value);
    }
}
