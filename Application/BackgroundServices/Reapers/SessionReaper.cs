using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Betalish.Application.BackgroundServices.Reapers
{
    public class SessionReaper(
        IDateService dateService,
        IServiceProvider serviceProvider) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken cancellation)
        {
            while (!cancellation.IsCancellationRequested)
            {
                try
                {
                    await Reap(cancellation);
                }
                catch
                {
                    // TODO: Use the plain logger
                }

                await Task
                    .Delay(TimeSpan.FromMinutes(5), cancellation)
                    .ConfigureAwait(false);

                // NOTE: Should we ConfigureAwait(false)?
                // I don't now. Read this?
                // https://devblogs.microsoft.com/dotnet/configureawait-faq/
            }
        }

        private async Task Reap(CancellationToken cancellation)
        {
            using var scope = serviceProvider.CreateScope();

            var database = scope.ServiceProvider
                .GetRequiredService<IDatabaseService>();

            var yesterday = dateService.GetDateTimeNow().AddDays(-1);

            var staleSessions = await database.Sessions
                .Where(x =>
                    x.Created < yesterday &&
                    !x.SessionActivities.Any(y => y.Created > yesterday))
                .ToListAsync(cancellation);

            var records = staleSessions
                .Select(x => new SessionRecord()
                {
                    UserId = x.UserId,
                    Login = x.Created!.Value,
                    Logout = dateService.GetDateTimeNow(),
                    IpAddress = x.IpAddress,
                    ClientId = x.ClientId,
                    SignInBy = x.SignInBy,
                    SessionEnd = SessionEnd.ReapedByTimeout,
                });

            database.SessionRecords.AddRange(records);

            database.Sessions.RemoveRange(staleSessions);

            await database.SaveAsync(new NoUserToken(), cancellation);
        }
    }
}
