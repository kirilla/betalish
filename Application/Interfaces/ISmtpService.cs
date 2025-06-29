using Betalish.Common.Settings;

namespace Betalish.Application.Interfaces;

public interface ISmtpService
{
    void SendEmailMessage(
        SmtpConfiguration smtpConfiguration,
        EmailMessage email,
        List<EmailAttachment> attachments,
        List<EmailImage> images);

    void SendClientEmailMessage(
        SmtpConfiguration smtpConfiguration,
        ClientEmailMessage email);
}
