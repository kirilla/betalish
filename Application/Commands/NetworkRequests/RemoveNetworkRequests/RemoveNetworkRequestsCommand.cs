namespace Betalish.Application.Commands.NetworkRequests.RemoveNetworkRequests;

public class RemoveNetworkRequestsCommand(IDatabaseService database) : IRemoveNetworkRequestsCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveNetworkRequestsCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        await database.NetworkRequests.ExecuteDeleteAsync();
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAdmin;
    }
}
