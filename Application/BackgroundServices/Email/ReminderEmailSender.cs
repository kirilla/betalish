using Betalish.Application.EmailTemplates.ReminderEmail;
using Betalish.Application.Queues.LogItems;
using Betalish.Common.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Betalish.Application.BackgroundServices.Email;

public class ReminderEmailSender(
    IDateService dateService,
    ILogItemList logItemList,
    IServiceProvider serviceProvider,
    IOptions<DistributionConfiguration> options,
    IReminderEmailTemplate reminderTemplate) : BackgroundService
{
    private readonly DistributionConfiguration _config = options.Value;

    protected override async Task ExecuteAsync(CancellationToken cancellation)
    {
        while (!cancellation.IsCancellationRequested)
        {
            if (_config.Active)
                await SendOne(cancellation);

            await Task
                .Delay(TimeSpan.FromMinutes(5), cancellation)
                .ConfigureAwait(false);
        }
    }

    private async Task SendOne(CancellationToken cancellation)
    {
        using var scope = serviceProvider.CreateScope();

        var database = scope.ServiceProvider
            .GetRequiredService<IDatabaseService>();

        var smtpService = scope.ServiceProvider
            .GetRequiredService<ISmtpService>();

        // TODO: Rate limiting

        var trigger = await database.DistributionTriggers
            .Where(x => 
                x.DistributionTriggerKind == DistributionTriggerKind.ReminderEmail &&
                x.DistributionStatus == DistributionStatus.Pending)
            .OrderBy(x => x.Created)
            .Take(1)
            .SingleOrDefaultAsync(cancellation);

        if (trigger == null)
            return;

        var invoice = await database.Invoices
            .AsNoTracking()
            .Where(x => x.Id == trigger.InvoiceId)
            .SingleOrDefaultAsync(cancellation) ?? 
            throw new NotFoundException();

        if (string.IsNullOrWhiteSpace(invoice.Customer_Name))
            throw new MissingNameException();

        if (string.IsNullOrWhiteSpace(invoice.Customer_Email))
            throw new MissingEmailException();

        var invoicePlan = await database.InvoicePlans
            .Where(x => x.Id == trigger.InvoiceId)
            .SingleOrDefaultAsync(cancellation) ??
            throw new NotFoundException();

        var client = await database.Clients
            .AsNoTracking()
            .Where(x => x.Id == invoice.ClientId)
            .SingleOrDefaultAsync(cancellation) ??
            throw new NotFoundException();

        var emailAccount = await database.EmailAccounts
            .AsNoTracking()
            .Where(x => x.ClientId == invoice.ClientId)
            .SingleOrDefaultAsync(cancellation) ??
            throw new NotFoundException();

        var emailMessage = reminderTemplate.Create(
            emailAccount, invoice, invoicePlan);

        try
        {
            smtpService.SendEmailMessage(emailAccount, emailMessage);

            trigger.DistributionStatus = DistributionStatus.Distributed;
            trigger.Distributed = dateService.GetDateTimeNow();
        }
        catch (Exception ex)
        {
            trigger.DistributionStatus = DistributionStatus.Failed;

            logItemList.AddLogItem(new LogItem(ex)
            {
                ClientId = invoice.ClientId,
                Description = 
                    $"Trigger {trigger.Id}, " +
                    $"Client: {client.Name}, " +
                    $"InvoiceNumber: {invoice.InvoiceNumber}",
                Source = nameof(ReminderEmailSender),
            });
        }

        await database.SaveAsync(new NoUserToken(), cancellation);
    }
}
