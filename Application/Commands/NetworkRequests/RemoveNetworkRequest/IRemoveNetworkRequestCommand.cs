namespace Betalish.Application.Commands.NetworkRequests.RemoveNetworkRequest;

public interface IRemoveNetworkRequestCommand
{
    Task Execute(IUserToken userToken, RemoveNetworkRequestCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
