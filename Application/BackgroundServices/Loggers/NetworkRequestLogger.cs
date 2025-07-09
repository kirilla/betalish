using Betalish.Application.Queues.NetworkRequests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Betalish.Application.BackgroundServices.Loggers;

public class NetworkRequestLogger(IServiceProvider serviceProvider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await SaveEvents(stoppingToken);
            }
            catch
            {
                // TODO: Log exception to file?
            }

            await Task
                .Delay(TimeSpan.FromSeconds(20), stoppingToken)
                .ConfigureAwait(false);

            // NOTE: Should we ConfigureAwait(false)?
            // I don't now. Read this?
            // https://devblogs.microsoft.com/dotnet/configureawait-faq/
        }
    }

    private async Task SaveEvents(CancellationToken stoppingToken)
    {
        using var scope = serviceProvider.CreateScope();

        var networkRequestList = scope.ServiceProvider.GetRequiredService<INetworkRequestList>();

        var requests = networkRequestList.Take();

        if (requests.Count == 0)
            return;

        requests = Dedup(requests);

        var database = scope.ServiceProvider.GetRequiredService<IDatabaseService>();

        database.NetworkRequests.AddRange(requests);

        await database.SaveAsync(new NoUserToken());
    }

    public static List<NetworkRequest> Dedup(List<NetworkRequest> requests)
    {
        return requests
            .GroupBy(x => new
            {
                x.Url,
                x.Method,
                x.IpAddress,
                x.UserAgent,
                x.Blocked,
            })
            .ToList()
            .Select(x => new NetworkRequest()
            {
                Url = x.Key.Url,
                Method = x.Key.Method,
                IpAddress = x.Key.IpAddress,
                UserAgent = x.Key.UserAgent,
                Blocked = x.Key.Blocked,
                RepeatCount = x.Count() == 1 ? null : x.Count(),
                //RepeatedUntil = x.Count() == 1 ? null : x.Max(x => x.Created),
            })
            .ToList();
    }
}
