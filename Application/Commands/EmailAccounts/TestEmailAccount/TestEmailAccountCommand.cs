namespace Betalish.Application.Commands.EmailAccounts.TestEmailAccount;

public class TestEmailAccountCommand(
    IDatabaseService database,
    ISmtpService smtpService) : ITestEmailAccountCommand
{
    public async Task Execute(
        IUserToken userToken, TestEmailAccountCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        var emailAccount = await database.EmailAccounts
            .AsNoTracking()
            .Where(x =>
                x.Id == model.Id &&
                x.ClientId == userToken.ClientId!.Value)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        var emailMessage = CreateEmailMesasge(
            model.ToName,
            model.ToAddress,
            emailAccount!);

        smtpService.SendEmailMessage(emailAccount, emailMessage);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient && // NOTE: Both
            userToken.IsAdmin;
    }

    private EmailMessage CreateEmailMesasge(
        string toName,
        string toAddress,
        EmailAccount account)
    {
        var email = new EmailMessage()
        {
            ToName = toName,
            ToAddress = toAddress,

            FromName = account.FromName,
            FromAddress = account.FromAddress,

            ReplyToName = null,
            ReplyToAddress = null,

            Subject = "Faktura",

            HtmlBody = CreateHtmlBody(toName, toAddress, account),
            TextBody = CreateTextBody(toName, toAddress, account),

            EmailStatus = EmailStatus.NotSent,
            //EmailKind = EmailKind.SignupForService,
        };

        return email;
    }

    private string CreateHtmlBody(
        string toName,
        string toAddress,
        EmailAccount account)
    {
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
                <h3>Här kommer ett testmeddelande!</h3>
                <p>
                    Mvh, Betalish.
                </p>
            </body>
            """;
    }

    private string CreateTextBody(
        string toName,
        string toAddress,
        EmailAccount account)
    {
        return $"""
            Här kommer ett testmeddelande!
            
            Mvh, Betalish.
            """;
    }
}
