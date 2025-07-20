using Betalish.Application.Queues.LogItems;
using Betalish.Common.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Betalish.Application.BackgroundServices.Reapers
{
    public class NetworkRequestReaper(
        ILogItemList logItemList,
        IServiceProvider serviceProvider,
        IOptions<FirewallConfiguration> firewallOptions) : BackgroundService
    {
        private readonly int _historySize = firewallOptions.Value.HistorySize;

        protected override async Task ExecuteAsync(CancellationToken cancellation)
        {
            while (!cancellation.IsCancellationRequested)
            {
                try
                {
                    await Reap(cancellation);
                }
                catch (Exception ex)
                {
                    logItemList.AddLogItem(new LogItem(ex)
                    {
                        LogItemKind = LogItemKind.NetworkRequestReaperFailed,
                    });
                }

                await Task
                    .Delay(TimeSpan.FromSeconds(10), cancellation)
                    .ConfigureAwait(false);

                // NOTE: Should we ConfigureAwait(false)?
                // I don't now. Read this?
                // https://devblogs.microsoft.com/dotnet/configureawait-faq/
            }
        }

        private async Task Reap(CancellationToken cancellation)
        {
            using var scope = serviceProvider.CreateScope();

            var database = scope.ServiceProvider
                .GetRequiredService<IDatabaseService>();

            var count = await database.NetworkRequests
                .OrderByDescending(x => x.Created)
                .Skip(_historySize * 2)
                .CountAsync(cancellation);

            if (count == 0)
                return;

            await database.NetworkRequests
                .OrderByDescending(x => x.Created)
                .Skip(_historySize)
                .ExecuteDeleteAsync(cancellation);

            logItemList.AddLogItem(new LogItem()
            {
                Description = $"{count} logged network requests removed.",
                LogItemKind = LogItemKind.NetworkRequestReaped,
            });
        }
    }
}
