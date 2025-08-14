namespace Betalish.Application.EmailTemplates.CreditInvoiceEmail;

public class CreditInvoiceEmailTemplate : ICreditInvoiceEmailTemplate
{
    public EmailMessage Create(EmailAccount account, Invoice invoice)
    {
        var email = new EmailMessage()
        {
            ToName = invoice.Customer_Name,
            ToAddress = invoice.Customer_Email!,

            FromName = account.FromName,
            FromAddress = account.FromAddress,

            ReplyToName = account.ReplyToName,
            ReplyToAddress = account.ReplyToAddress,

            Subject = "Kreditering",

            HtmlBody = CreateHtmlBody(account, invoice),
            TextBody = CreateTextBody(account, invoice),
        };

        return email;
    }

    private string CreateHtmlBody(EmailAccount account, Invoice invoice)
    {
        return $"""
            {Email.Html.Start}
            <h5>Kreditfaktura från ???</h5>
            <ul>
                <li>
                    Fakturanr: {invoice.InvoiceNumber?.ToSwedish() ?? "saknas"}
                </li>
                <li>
                    Tillgodo: {invoice.Balance.ToSwedish()}
                </li>
            </ul>
            <p>
                Mvh, Betalish.
            </p>
            {Email.Html.End}
            """;
    }

    private string CreateTextBody(EmailAccount account, Invoice invoice)
    {
        return $"""
            En kreditfaktura från ???
            
            Fakturanr: {invoice.InvoiceNumber?.ToSwedish() ?? "saknas"}
            Tillgodo: {invoice.Balance.ToSwedish()}
            
            Mvh, Betalish.
            """;
    }
}
