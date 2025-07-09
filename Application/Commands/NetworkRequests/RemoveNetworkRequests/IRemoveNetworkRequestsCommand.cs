namespace Betalish.Application.Commands.NetworkRequests.RemoveNetworkRequests;

public interface IRemoveNetworkRequestsCommand
{
    Task Execute(IUserToken userToken, RemoveNetworkRequestsCommandModel model);

    Task<bool> IsPermitted(IUserToken userToken);
}
