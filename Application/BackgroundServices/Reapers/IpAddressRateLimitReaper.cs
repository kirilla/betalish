using Betalish.Application.Queues.LogItems;
using Betalish.Application.Queues.RateLimiting;
using Microsoft.Extensions.Hosting;

namespace Betalish.Application.BackgroundServices.Reapers
{
    public class IpAddressRateLimitReaper(
        IDateService dateService,
        ILogItemList logItemList,
        IIpAddressRateLimiter rateLimiter,
        IServiceProvider serviceProvider) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Reap(stoppingToken);

                await Task
                    .Delay(TimeSpan.FromMinutes(1), stoppingToken)
                    .ConfigureAwait(false);

                // NOTE: Should we ConfigureAwait(false)?
                // I don't now. Read this?
                // https://devblogs.microsoft.com/dotnet/configureawait-faq/
            }
        }

        private async Task Reap(CancellationToken stoppingToken)
        {
            rateLimiter.RemoveOlderThan(TimeSpan.FromMinutes(6));
        }
    }
}
