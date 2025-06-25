using Betalish.Application.Auth;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Betalish.Application.BackgroundServices.Reapers
{
    public class SessionReaper(
        IDateService dateService,
        IServiceProvider serviceProvider) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await ReapSessions(stoppingToken);
                }
                catch
                {
                    // TODO: Use the plain logger
                }

                await Task
                    .Delay(TimeSpan.FromMinutes(5), stoppingToken)
                    .ConfigureAwait(false);

                // NOTE: Should we ConfigureAwait(false)?
                // I don't now. Read this?
                // https://devblogs.microsoft.com/dotnet/configureawait-faq/
            }
        }

        private async Task ReapSessions(CancellationToken stoppingToken)
        {
            using var scope = serviceProvider.CreateScope();

            var database = scope.ServiceProvider
                .GetRequiredService<IDatabaseService>();

            var yesterday = dateService.GetDateTimeNow().AddDays(-1);

            var staleSessions = await database.Sessions
                .Where(x =>
                    x.Created < yesterday &&
                    !x.SessionActivities.Any(y => y.Created > yesterday))
                .ToListAsync(stoppingToken);

            var records = staleSessions
                .Select(x => new SessionRecord()
                {
                    UserId = x.UserId,
                    Login = x.Created!.Value,
                    Logout = dateService.GetDateTimeNow(),
                    WasReaped = true,
                    IpAddress = x.IpAddress,
                });

            database.SessionRecords.AddRange(records);

            database.Sessions.RemoveRange(staleSessions);

            await database.SaveAsync(new NoUserToken());

            // Prune 
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
