namespace Betalish.Application.Commands.BlockedRequests.RemoveBlockedRequest;

public interface IRemoveBlockedRequestCommand
{
    Task Execute(IUserToken userToken, RemoveBlockedRequestCommandModel model);

    Task<bool> IsPermitted(IUserToken userToken);
}
