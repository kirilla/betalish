namespace Betalish.Application.EmailTemplates.DebitInvoiceEmail
{
    public interface IDebitInvoiceEmailTemplate
    {
        EmailMessage Create(EmailAccount account, Invoice invoice);
    }
}
