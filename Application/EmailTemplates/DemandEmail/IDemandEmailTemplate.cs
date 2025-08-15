namespace Betalish.Application.EmailTemplates.DemandEmail
{
    public interface IDemandEmailTemplate
    {
        EmailMessage Create(
            EmailAccount account, 
            Invoice invoice, 
            InvoicePlan invoicePlan);
    }
}
