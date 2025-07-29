using Microsoft.Extensions.Hosting;

namespace Betalish.Application.Queues.NetworkRequests;

public class NetworkRequestList(
    IHostEnvironment hostEnvironment,
    IDateService dateService) : INetworkRequestList
{
    private List<NetworkRequest> List { get; } = [];

    public void Add(NetworkRequest request)
    {
        if (!hostEnvironment.IsProduction())
            return;

        lock (this)
        {
            request.Created = dateService.GetDateTimeNow();

            List.Add(request);
        }
    }

    public List<NetworkRequest> Take()
    {
        lock (this)
        {
            var entries = List.ToList();

            List.Clear();

            return entries;
        }
    }
}
