using Betalish.Application.Queues.LogItems;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Betalish.Application.BackgroundServices.Reapers
{
    public class SignupReaper(
        IDateService dateService,
        ILogItemList logItemList,
        IServiceProvider serviceProvider) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken cancellation)
        {
            while (!cancellation.IsCancellationRequested)
            {
                await ReapSignups(cancellation);

                await Task
                    .Delay(TimeSpan.FromMinutes(28), cancellation)
                    .ConfigureAwait(false);

                // NOTE: Should we ConfigureAwait(false)?
                // I don't now. Read this?
                // https://devblogs.microsoft.com/dotnet/configureawait-faq/
            }
        }

        private async Task ReapSignups(CancellationToken cancellation)
        {
            using var scope = serviceProvider.CreateScope();

            var database = scope.ServiceProvider
                .GetRequiredService<IDatabaseService>();

            var timeInThePast = dateService.GetDateTimeNow().AddDays(-1);

            var signups = await database.Signups
               .Where(x => x.Created < timeInThePast)
               .ToListAsync(cancellation);

            if (signups.Count == 0)
                return;

            logItemList.AddLogItem(new LogItem()
            {
                Description = $"{signups.Count} signups removed.",
                LogItemKind = LogItemKind.SignupsReaped,
            });

            database.Signups.RemoveRange(signups);
            
            await database.SaveAsync(new NoUserToken(), cancellation);
        }
    }
}
