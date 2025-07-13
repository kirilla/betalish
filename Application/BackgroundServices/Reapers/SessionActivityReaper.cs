using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Betalish.Application.BackgroundServices.Reapers
{
    public class SessionActivityReaper(
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
                    .Delay(TimeSpan.FromMinutes(3), cancellation)
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

            var activitiesToRemove = new List<SessionActivity>();

            var activities = await database.SessionActivities
                .ToListAsync(cancellation);

            if (activities.Count == 0)
                return;

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

            await database.SaveAsync(new NoUserToken(), cancellation);
        }
    }
}
