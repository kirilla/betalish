using Betalish.Application.Queues.LogItems;
using Betalish.Application.Queues.EndpointRateLimiting;
using Microsoft.Extensions.Hosting;

namespace Betalish.Application.BackgroundServices.Reapers
{
    public class EndpointRateLimitReaper(
        IDateService dateService,
        ILogItemList logItemList,
        IEndpointRateLimiter rateLimiter,
        IServiceProvider serviceProvider) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Reap(stoppingToken);

                await Task
                    .Delay(TimeSpan.FromMinutes(1), stoppingToken)
                    .ConfigureAwait(false);

                // NOTE: Should we ConfigureAwait(false)?
                // I don't now. Read this?
                // https://devblogs.microsoft.com/dotnet/configureawait-faq/
            }
        }

        private void Reap(CancellationToken stoppingToken)
        {
            rateLimiter.RemoveOlderThan(TimeSpan.FromMinutes(6));
        }
    }
}
