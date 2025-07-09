namespace Betalish.Application.Commands.NetworkRequests.RemoveNetworkRequest;

public interface IRemoveNetworkRequestCommand
{
    Task Execute(IUserToken userToken, RemoveNetworkRequestCommandModel model);

    Task<bool> IsPermitted(IUserToken userToken);
}
