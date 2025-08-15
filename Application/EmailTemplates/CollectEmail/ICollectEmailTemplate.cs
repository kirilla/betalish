namespace Betalish.Application.EmailTemplates.CollectEmail
{
    public interface ICollectEmailTemplate
    {
        EmailMessage Create(EmailAccount account, Invoice invoice);
    }
}
