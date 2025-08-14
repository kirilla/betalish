namespace Betalish.Application.EmailTemplates.CreditInvoiceEmail
{
    public interface ICreditInvoiceEmailTemplate
    {
        EmailMessage Create(EmailAccount account, Invoice invoice);
    }
}
