namespace Betalish.Application.Commands.BlockedRequests.RemoveBlockedRequests;

public interface IRemoveBlockedRequestsCommand
{
    Task Execute(IUserToken userToken, RemoveBlockedRequestsCommandModel model);

    Task<bool> IsPermitted(IUserToken userToken);
}
