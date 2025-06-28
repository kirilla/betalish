using Betalish.Application.Queues.LogItems;
using Betalish.Common.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Betalish.Application.BackgroundServices.Reapers
{
    public class BlockedRequestReaper(
        IDateService dateService,
        ILogItemList logItemList,
        IServiceProvider serviceProvider,
        IOptions<FirewallConfiguration> firewallOptions) : BackgroundService
    {
        private readonly int _historySize = firewallOptions.Value.HistorySize;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await Reap(stoppingToken);
                }
                catch (Exception ex)
                {
                    logItemList.AddLogItem(new LogItem(ex)
                    {
                        LogItemKind = LogItemKind.BlockedRequestReaperFailed,
                    });
                }

                await Task
                    .Delay(TimeSpan.FromSeconds(10), stoppingToken)
                    .ConfigureAwait(false);

                // NOTE: Should we ConfigureAwait(false)?
                // I don't now. Read this?
                // https://devblogs.microsoft.com/dotnet/configureawait-faq/
            }
        }

        private async Task Reap(CancellationToken stoppingToken)
        {
            using var scope = serviceProvider.CreateScope();

            var database = scope.ServiceProvider
                .GetRequiredService<IDatabaseService>();

            var count = await database.BlockedRequests
                .OrderByDescending(x => x.Created)
                .Skip(_historySize)
                .CountAsync();

            if (count == 0)
                return;

            await database.BlockedRequests
                .OrderByDescending(x => x.Created)
                .Skip(_historySize)
                .ExecuteDeleteAsync();

            logItemList.AddLogItem(new LogItem()
            {
                Description = $"{count} blocked requests reaped.",
                LogItemKind = LogItemKind.BlockedRequestReaped,
            });
        }
    }
}
