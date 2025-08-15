using Betalish.Common.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Betalish.Application.BackgroundServices.DistributionSchedulers;

public class ReminderEmailDistributionScheduler(
    IDateService dateService,
    IServiceProvider serviceProvider,
    IOptions<DistributionConfiguration> options) : BackgroundService
{
    private readonly DistributionConfiguration _config = options.Value;

    protected override async Task ExecuteAsync(CancellationToken cancellation)
    {
        while (!cancellation.IsCancellationRequested)
        {
            if (_config.Active)
                await Generate(cancellation);

            await Task
                .Delay(TimeSpan.FromMinutes(5), cancellation)
                .ConfigureAwait(false);
        }
    }

    private async Task Generate(CancellationToken cancellation)
    {
        using var scope = serviceProvider.CreateScope();

        var database = scope.ServiceProvider
            .GetRequiredService<IDatabaseService>();

        var today = dateService.GetDateOnlyToday();

        var invoices = await database.Invoices
            .AsNoTracking()
            .Where(x =>
                x.Customer_Email != null &&
                x.Reminder == true &&
                x.ReminderDate <= today &&
                x.InvoiceStatus == InvoiceStatus.Issued && 
                !x.DistributionTriggers.Any(y => 
                    y.DistributionTriggerKind == DistributionTriggerKind.ReminderEmail))
            .Take(10)
            .ToListAsync(cancellation);

        var triggers = invoices
            .Select(x => new DistributionTrigger()
            {
                DistributionTriggerKind = DistributionTriggerKind.ReminderEmail,
                DistributionStatus = DistributionStatus.Pending,
                InvoiceId = x.Id,
            })
            .ToList();

        database.DistributionTriggers.AddRange(triggers);

        await database.SaveAsync(new NoUserToken(), cancellation);
    }
}
