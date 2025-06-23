using Betalish.Common.Settings;

namespace Betalish.Application.Interfaces;

public interface ISmtpService
{
    void SendMessage(
        SmtpConfiguration smtpConfiguration,
        EmailMessage email,
        List<EmailAttachment> attachments,
        List<EmailImage> images);

    void SendMessage(
        SmtpConfiguration smtpConfiguration,
        ClientEmailMessage email);
}
