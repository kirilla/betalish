using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Betalish.Application.BackgroundServices.Reapers
{
    public class SessionActivityReaper(
        IDateService dateService,
        IServiceProvider serviceProvider) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await Reap(stoppingToken);
                }
                catch
                {
                    // TODO: Use the plain logger
                }

                await Task
                    .Delay(TimeSpan.FromMinutes(3), stoppingToken)
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

            var activitiesToRemove = new List<SessionActivity>();

            var activities = await database.SessionActivities.ToListAsync();

            var groups = activities
                .GroupBy(x => x.SessionId)
                .Where(x => x.Count() > 1)
                .ToList();

            foreach (var group in groups)
            {
                var redundant = group
                    .OrderBy(x => x.Created)
                    .SkipLast(1)
                    .ToList();

                activitiesToRemove.AddRange(redundant);
            }

            database.SessionActivities.RemoveRange(activitiesToRemove);

            await database.SaveAsync(new NoUserToken());
        }
    }
}
