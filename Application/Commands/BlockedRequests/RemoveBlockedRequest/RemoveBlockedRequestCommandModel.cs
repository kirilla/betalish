namespace Betalish.Application.Commands.BlockedRequests.RemoveBlockedRequest;

public class RemoveBlockedRequestCommandModel
{
    public int BlockedRequestId { get; set; }

    public bool Confirmed { get; set; }
}
