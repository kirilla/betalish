using Betalish.Application.Queues.EndpointRateLimiting;
using Microsoft.Extensions.Hosting;

namespace Betalish.Application.BackgroundServices.Reapers
{
    public class EndpointRateLimitReaper(
        IEndpointRateLimiter rateLimiter) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken cancellation)
        {
            while (!cancellation.IsCancellationRequested)
            {
                Reap();

                await Task
                    .Delay(TimeSpan.FromMinutes(1), cancellation)
                    .ConfigureAwait(false);

                // NOTE: Should we ConfigureAwait(false)?
                // I don't now. Read this?
                // https://devblogs.microsoft.com/dotnet/configureawait-faq/
            }
        }

        private void Reap()
        {
            rateLimiter.RemoveOlderThan(TimeSpan.FromMinutes(6));
        }
    }
}
