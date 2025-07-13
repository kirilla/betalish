using Betalish.Application.Queues.SessionActivities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Betalish.Application.BackgroundServices.Loggers;

public class SessionActivityLogger(IServiceProvider serviceProvider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken cancellation)
    {
        while (!cancellation.IsCancellationRequested)
        {
            try
            {
                await SaveEvents(cancellation);
            }
            catch
            {
                // TODO: Log event?
            }

            await Task
                .Delay(TimeSpan.FromSeconds(30), cancellation)
                .ConfigureAwait(false);

            // NOTE: Should we ConfigureAwait(false)?
            // I don't now. Read this?
            // https://devblogs.microsoft.com/dotnet/configureawait-faq/
        }
    }

    private async Task SaveEvents(CancellationToken cancellation)
    {
        using var scope = serviceProvider.CreateScope();

        var list = scope.ServiceProvider
            .GetRequiredService<ISessionActivityList>();

        var activeSessionIds = list.RemoveSessionIds();

        if (activeSessionIds.Count == 0)
            return;

        var database = scope.ServiceProvider
            .GetRequiredService<IDatabaseService>();

        var storedSessionId = await database.Sessions
            .Select(x => x.Id)
            .Distinct()
            .ToListAsync(cancellation);

        var idsToLog = storedSessionId
            .Intersect(activeSessionIds)
            .Distinct()
            .ToList();

        var sessionActivities = idsToLog
            .Select(x => new SessionActivity()
            {
                SessionId = x,
            })
            .ToList();

        database.SessionActivities.AddRange(sessionActivities);

        await database.SaveAsync(new NoUserToken(), cancellation);
    }
}
