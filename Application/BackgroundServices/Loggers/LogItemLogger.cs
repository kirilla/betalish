using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Betalish.Application.Queues.LogItems;
using Betalish.Application.Auth;

namespace Betalish.Application.BackgroundServices.Loggers;

public class LogItemLogger(IServiceProvider serviceProvider) : BackgroundService
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

        var logItemList = scope.ServiceProvider.GetRequiredService<ILogItemList>();

        var logItems = logItemList.TakeLogItems();

        if (logItems.Count == 0)
            return;

        var database = scope.ServiceProvider.GetRequiredService<IDatabaseService>();

        database.LogItems.AddRange(logItems);

        await database.SaveAsync(new NoUserToken());
    }
}
