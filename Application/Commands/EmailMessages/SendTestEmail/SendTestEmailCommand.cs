namespace Betalish.Application.Commands.EmailMessages.SendTestEmail;

public class SendTestEmailCommand(IDatabaseService database) : ISendTestEmailCommand
{
    public async Task Execute(
        IUserToken userToken, SendTestEmailCommandModel model)
    {
        if (!await IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var message = new EmailMessage()
        {
            ToName = model.ToName,
            ToAddress = model.ToAddress,
            FromName = model.FromName,
            FromAddress = model.FromAddress,
            ReplyToName = model.ReplyToName,
            ReplyToAddress = model.ReplyToAddress,
            Subject = model.Subject,
            HtmlBody = model.HtmlBody,
            TextBody = model.TextBody,
            EmailStatus = EmailStatus.NotSent,
            //EmailTemplateKind = null,
        };

        database.EmailMessages.Add(message);

        await database.SaveAsync(userToken);
    }

    public async Task<bool> IsPermitted(IUserToken userToken)
    {
        return await database.AdminAuths.AnyAsync(x =>
            x.UserId == userToken.UserId!.Value);
    }
}
