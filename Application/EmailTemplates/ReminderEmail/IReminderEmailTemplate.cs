namespace Betalish.Application.EmailTemplates.ReminderEmail
{
    public interface IReminderEmailTemplate
    {
        EmailMessage Create(EmailAccount account, Invoice invoice);
    }
}
