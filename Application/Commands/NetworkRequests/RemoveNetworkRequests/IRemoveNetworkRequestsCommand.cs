namespace Betalish.Application.Commands.NetworkRequests.RemoveNetworkRequests;

public interface IRemoveNetworkRequestsCommand
{
    Task Execute(IUserToken userToken, RemoveNetworkRequestsCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
