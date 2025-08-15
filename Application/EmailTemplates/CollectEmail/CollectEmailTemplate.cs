namespace Betalish.Application.EmailTemplates.CollectEmail;

public class CollectEmailTemplate : ICollectEmailTemplate
{
    public EmailMessage Create(
        EmailAccount account,
        Invoice invoice,
        InvoicePlan invoicePlan)
    {
        var email = new EmailMessage()
        {
            ToName = invoice.Customer_Name,
            ToAddress = invoice.Customer_Email!,

            FromName = account.FromName,
            FromAddress = account.FromAddress,

            ReplyToName = account.ReplyToName,
            ReplyToAddress = account.ReplyToAddress,

            Subject = "Anmälan om kronofogdeärende",

            HtmlBody = CreateHtmlBody(account, invoice, invoicePlan),
            TextBody = CreateTextBody(account, invoice, invoicePlan),
        };

        return email;
    }

    private string CreateHtmlBody(
        EmailAccount account,
        Invoice invoice,
        InvoicePlan invoicePlan)
    {
        return $"""
            {Email.Html.Start}
            <h2>Kronofogdeärende</h5>
            <ul>
                <li>
                    Faktura {invoice.InvoiceNumber?.ToSwedish() ?? "saknas"} 
                    har förfallit till betalning. Påminnelse och krav har utgått,
                    men inte hörsammats.
                </li>
                <li>
                    Ett kronofogdeären kan komma att inledas.
                </li>
            </ul>
            <p>
                Mvh, Betalish.
            </p>
            {Email.Html.End}
            """;
    }

    private string CreateTextBody(
        EmailAccount account,
        Invoice invoice,
        InvoicePlan invoicePlan)
    {
        return $"""
            Kronofogdeärende
            
            Faktura {invoice.InvoiceNumber?.ToSwedish() ?? "saknas"} 
            har förfallit till betalning. Påminnelse och krav har utgått,
            men inte hörsammats.

            Ett kronofogdeären kan komma att inledas.
            
            Mvh, Betalish.
            """;
    }
}
