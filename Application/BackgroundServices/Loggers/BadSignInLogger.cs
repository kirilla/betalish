using Betalish.Application.Queues.BadSignIns;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Betalish.Application.BackgroundServices.Loggers;

public class BadSignInLogger(IServiceProvider serviceProvider) : BackgroundService
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

        var badSignInList = scope.ServiceProvider
            .GetRequiredService<IBadSignInList>();

        var signIns = badSignInList.TakeBadSignIns();

        if (signIns.Count == 0)
            return;

        var database = scope.ServiceProvider
            .GetRequiredService<IDatabaseService>();

        database.BadSignIns.AddRange(signIns);

        await database.SaveAsync(new NoUserToken(), cancellation);
    }
}
