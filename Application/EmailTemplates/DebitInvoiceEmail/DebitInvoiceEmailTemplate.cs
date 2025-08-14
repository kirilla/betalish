namespace Betalish.Application.EmailTemplates.DebitInvoiceEmail;

public class DebitInvoiceEmailTemplate : IDebitInvoiceEmailTemplate
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

            Subject = "Faktura",

            HtmlBody = CreateHtmlBody(account, invoice),
            TextBody = CreateTextBody(account, invoice),
        };

        return email;
    }

    private string CreateHtmlBody(
        EmailAccount account,
        Invoice invoice)
    {
        return $"""
            {Email.Html.Start}
            <h5>Faktura från ???</h5>
            <ul>
                <li>
                    Fakturanr: {invoice.InvoiceNumber?.ToSwedish() ?? "saknas"}
                </li>
                <li>
                    Att betala: {invoice.LeftToPay.ToSwedish()}
                </li>
                <li>
                    Senast: {invoice.DueDate.ToIso8601()}
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
        Invoice invoice)
    {
        return $"""
            En faktura från ???
            
            Fakturanr: {invoice.InvoiceNumber?.ToSwedish() ?? "saknas"}

            Att betala: {invoice.LeftToPay.ToSwedish()}
            Senast: {invoice.DueDate.ToIso8601()}
            
            Mvh, Betalish.
            """;
    }
}
