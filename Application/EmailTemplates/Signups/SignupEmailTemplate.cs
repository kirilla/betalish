using Microsoft.Extensions.Options;
using Betalish.Common.Settings;

namespace Betalish.Application.EmailTemplates.Signups;

public class SignupEmailTemplate : ISignupEmailTemplate
{
    private readonly SmtpConfiguration _configuration;

    public SignupEmailTemplate(
        IOptions<SmtpConfiguration> options)
    {
        _configuration = options.Value;
    }

    public EmailMessage Create(Signup signup)
    {
        var email = new EmailMessage()
        {
            ToName = signup.PersonName,
            ToAddress = signup.EmailAddress,

            FromName = _configuration.FromName,
            FromAddress = _configuration.FromAddress,

            ReplyToName = null,
            ReplyToAddress = null,

            Subject = "Ansökan mottagen",

            HtmlBody = CreateHtmlBody(signup),
            TextBody = CreateTextBody(signup),

            EmailStatus = EmailStatus.NotSent,
            //EmailKind = EmailKind.SignupForService,
        };

        return email;
    }

    private string CreateHtmlBody(Signup signup)
    {
        var guid = signup.Guid.ToString()!.ToLower();

        return $"""
            <!DOCTYPE html>
            <html lang="sv-SE">
            <head>
                <meta charset="utf-8">
                <meta name="viewport" content="width=device-width">
            <style>
            </style>
            </head>
            <body style="background-color:white">
                <h5>Hej och välkommen!</h5>
                <p>
                    <a href="https://betalish.se/finish-signup/{guid}">Skapa ditt konto nu</a>
                </p>
                <p>
                    Mvh, Betalish.
                </p>
            </body>
            """;
    }

    private string CreateTextBody(Signup signup)
    {
        var guid = signup.Guid.ToString()!.ToLower();

        return $"""
            Hej och välkommen!
            
            Skapa ditt konto nu
            https://betalish.se/finish-signup/{guid}
            
            Mvh, Betalish.
            """;
    }
}
