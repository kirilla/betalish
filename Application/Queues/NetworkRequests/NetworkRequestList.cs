using Microsoft.Extensions.Hosting;

namespace Betalish.Application.Queues.NetworkRequests;

public class NetworkRequestList(
    IHostEnvironment hostEnvironment,
    IDateService dateService) : INetworkRequestList
{
    private List<NetworkRequest> list { get; } = [];

    public void Add(NetworkRequest request)
    {
        if (!hostEnvironment.IsProduction())
            return;

        lock (this)
        {
            request.Created = dateService.GetDateTimeNow();

            list.Add(request);
        }
    }

    public List<NetworkRequest> Take()
    {
        lock (this)
        {
            var entries = list.ToList();

            list.Clear();

            return entries;
        }
    }
}
