using Betalish.Application.Queues.UserEvents;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Betalish.Application.BackgroundServices.Loggers;

public class UserEventLogger(IServiceProvider serviceProvider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken cancellation)
    {
        while (!cancellation.IsCancellationRequested)
        {
            try
            {
                await Store(cancellation);
            }
            catch
            {
                // TODO: Log exception to file?
            }

            await Task
                .Delay(TimeSpan.FromMinutes(1), cancellation)
                .ConfigureAwait(false);

            // NOTE: Should we ConfigureAwait(false)?
            // I don't now. Read this?
            // https://devblogs.microsoft.com/dotnet/configureawait-faq/
        }
    }

    private async Task Store(CancellationToken cancellation)
    {
        using var scope = serviceProvider.CreateScope();

        var listService = scope.ServiceProvider.GetRequiredService<IUserEventList>();

        var events = listService.TakeUserEvents();

        if (events.Count == 0)
            return;

        var database = scope.ServiceProvider.GetRequiredService<IDatabaseService>();

        database.UserEvents.AddRange(events);

        await database.SaveAsync(new NoUserToken(), cancellation);
    }
}
