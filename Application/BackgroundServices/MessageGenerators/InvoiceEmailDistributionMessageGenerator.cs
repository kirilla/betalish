using Betalish.Common.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Betalish.Application.BackgroundServices.MessageGenerators;

public class InvoiceEmailDistributionMessageGenerator(
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

        var invoicePlans = await database.InvoicePlans
            .Include(x => x.Invoice)
            .Where(x =>
                x.DistributionDate <= today &&
                x.DistributionEmailSent == null &&
                x.Invoice.InvoiceStatus == InvoiceStatus.Issued && 
                x.SendByEmail == true &&
                !x.Invoice.DistributionMessages.Any(y => 
                    y.DistributionMessageKind == DistributionMessageKind.InvoiceEmail))
            .Take(10)
            .ToListAsync(cancellation);

        var messages = invoicePlans
            .Select(x => new DistributionMessage()
            {
                DistributionMessageKind = DistributionMessageKind.InvoiceEmail,
                DistributionStatus = DistributionStatus.Pending,
                InvoiceId = x.Id,
            })
            .ToList();

        database.DistributionMessages.AddRange(messages);

        await database.SaveAsync(new NoUserToken(), cancellation);
    }
}
