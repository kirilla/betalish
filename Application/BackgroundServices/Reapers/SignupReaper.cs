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
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await ReapSignups(stoppingToken);

                await Task
                    .Delay(TimeSpan.FromMinutes(28), stoppingToken)
                    .ConfigureAwait(false);

                // NOTE: Should we ConfigureAwait(false)?
                // I don't now. Read this?
                // https://devblogs.microsoft.com/dotnet/configureawait-faq/
            }
        }

        private async Task ReapSignups(CancellationToken stoppingToken)
        {
            using var scope = serviceProvider.CreateScope();

            var database = scope.ServiceProvider
                .GetRequiredService<IDatabaseService>();

            var timeInThePast = dateService.GetDateTimeNow().AddDays(-1);

            var signups = await database.Signups
               .Where(x => x.Created < timeInThePast)
               .ToListAsync();

            var count = signups.Count;

            database.Signups.RemoveRange(signups);
            
            await database.SaveAsync(new NoUserToken());

            logItemList.AddLogItem(new LogItem()
            {
                Description = $"{count} signups reaped.",
                LogItemKind = LogItemKind.SignupsReaped,
            });
        }
    }
}
