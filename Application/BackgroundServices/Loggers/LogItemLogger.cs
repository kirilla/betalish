using Betalish.Application.Queues.LogItems;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

        logItems = Dedup(logItems);

        var database = scope.ServiceProvider.GetRequiredService<IDatabaseService>();

        database.LogItems.AddRange(logItems);

        await database.SaveAsync(new NoUserToken());
    }

    public static List<LogItem> Dedup(List<LogItem> logItems)
    {
        return logItems
            .GroupBy(x => new
            {
                x.Error,
                x.Description,
                x.Exception,
                x.InnerException,
                x.LogItemKind,
                x.UserId,
                x.IpAddress,
            })
            .ToList()
            .Select(x => new LogItem()
            {
                Error = x.Key.Error,
                Description = x.Key.Description,
                Exception = x.Key.Exception,
                InnerException = x.Key.InnerException,
                LogItemKind = x.Key.LogItemKind,
                UserId = x.Key.UserId,
                IpAddress = x.Key.IpAddress,
                RepeatCount = x.Count() == 1 ? null : x.Count(),
                RepeatedUntil = x.Count() == 1 ? null : x.Max(x => x.Created),
            })
            .ToList();
    }
}
