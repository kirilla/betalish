using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Betalish.Common.Settings;

namespace Betalish.Application.BackgroundServices.Email;

public class EmailSender(
    IDateService dateService,
    ILogger<EmailSender> logger,
    IServiceProvider serviceProvider,
    IOptions<SmtpConfiguration> options) : BackgroundService
{
    private readonly SmtpConfiguration _config = options.Value;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_config.Active)
                await SendBatch(stoppingToken);

            await Task
                .Delay(TimeSpan.FromMinutes(1), stoppingToken)
                .ConfigureAwait(false);

            // NOTE: Should we ConfigureAwait(false)?
            // I don't now. Read this?
            // https://devblogs.microsoft.com/dotnet/configureawait-faq/
        }
    }

    private async Task SendBatch(CancellationToken stoppingToken)
    {
        using var scope = serviceProvider.CreateScope();

        var database = scope.ServiceProvider
            .GetRequiredService<IDatabaseService>();

        var smtpService = scope.ServiceProvider
            .GetRequiredService<ISmtpService>();

        var hourlyRateLimit = 60;

        var anHourAgo = dateService.GetDateTimeNow().AddHours(-1);

        var sentLastHour = await database.EmailMessages
            .CountAsync(x =>
                x.EmailStatus == EmailStatus.Sent &&
                x.Sent > anHourAgo,
                stoppingToken);

        var unsentEmails = await database.EmailMessages
            .Where(x => x.EmailStatus == EmailStatus.NotSent)
            .Take(hourlyRateLimit - sentLastHour)
            .ToListAsync(stoppingToken);

        foreach (var email in unsentEmails)
        {
            try
            {
                var attachments = new List<EmailAttachment>();
                var images = new List<EmailImage>();

                smtpService.SendEmailMessage(_config, email, attachments, images);

                email.EmailStatus = EmailStatus.Sent;
                email.Sent = dateService.GetDateTimeNow();
            }
            catch (Exception e)
            {
                logger.LogError(e, "Fel i EmailSender: {exception}", e.Message);

                email.EmailStatus = EmailStatus.SendFailed;
            }

            if (stoppingToken.IsCancellationRequested)
                break;
        }

        await database.SaveAsync(new NoUserToken());
    }
}
