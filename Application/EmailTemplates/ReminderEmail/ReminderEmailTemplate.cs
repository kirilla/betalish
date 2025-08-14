namespace Betalish.Application.EmailTemplates.ReminderEmail;

public class ReminderEmailTemplate : IReminderEmailTemplate
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

            Subject = "Påminnelse",

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
            <h5>Påminnelse från ???</h5>
            <ul>
                <li>
                    Fakturanr: {invoice.InvoiceNumber?.ToSwedish() ?? "saknas"}
                </li>
                <li>
                    Att betala: {invoice.LeftToPay.ToSwedish()} kr
                </li>
                <li>
                    Senast: {invoicePlan.ReminderDueDate.ToIso8601()}
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
            En påminnelse från ???
            
            Fakturanr: {invoice.InvoiceNumber?.ToSwedish() ?? "saknas"}

            Att betala: {invoice.LeftToPay.ToSwedish()} kr

            Senast: {invoicePlan.ReminderDueDate.ToIso8601()}
            
            Mvh, Betalish.
            """;
    }
}
