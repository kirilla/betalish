using Betalish.Common.Settings;
using Microsoft.Extensions.Options;

namespace Betalish.Application.Commands.EmailMessages.SendEmail;

public class SendEmailCommand(
    IDateService dateService,
    ISmtpService smtpService,
    IDatabaseService database,
    IOptions<SmtpConfiguration> smtpOptions) : ISendEmailCommand
{
    public async Task Execute(
        IUserToken userToken, SendEmailCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var smtpConfiguration = smtpOptions.Value;

        var message = await database.EmailMessages
            .Where(x => x.Id == model.EmailMessageId)
            .SingleOrDefaultAsync() ??
             throw new NotFoundException();

        var attachments = await database.EmailAttachments
            .Where(x => x.EmailMessageId == message.Id)
            .ToListAsync();

        var images = await database.EmailImages
            .Where(x => x.EmailMessageId == message.Id)
            .ToListAsync();

        try
        {
            smtpService.SendEmailMessage(smtpConfiguration, message, attachments, images);

            message.EmailStatus = EmailStatus.Sent;
            message.Sent = dateService.GetDateTimeNow();
        }
        catch
        {
            message.EmailStatus = EmailStatus.SendFailed;
        }
        finally
        {
            await database.SaveAsync(userToken);
        }
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAdmin;
    }
}
