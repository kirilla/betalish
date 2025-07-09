namespace Betalish.Application.Queues.NetworkRequests;

public interface INetworkRequestList
{
    void Add(NetworkRequest request);

    List<NetworkRequest> Take();
}
