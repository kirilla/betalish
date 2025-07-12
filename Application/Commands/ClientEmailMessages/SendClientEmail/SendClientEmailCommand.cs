using Betalish.Common.Settings;
using Microsoft.Extensions.Options;

namespace Betalish.Application.Commands.ClientEmailMessages.SendClientEmail;

public class SendClientEmailCommand(
    IDateService dateService,
    ISmtpService smtpService,
    IDatabaseService database,
    IOptions<SmtpConfiguration> smtpOptions) : ISendClientEmailCommand
{
    public async Task Execute(
        IUserToken userToken, SendClientEmailCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var smtpConfiguration = smtpOptions.Value;

        var message = await database.ClientEmailMessages
            .Where(x => 
                x.Id == model.Id &&
                x.ClientId == userToken.ClientId!.Value)
            .SingleOrDefaultAsync() ??
             throw new NotFoundException();

        try
        {
            smtpService.SendClientEmailMessage(smtpConfiguration, message);

            message.EmailStatus = EmailStatus.Sent;
            message.Sent = dateService.GetDateTimeNow();
        }
        catch (Exception e)
        {
            message.EmailStatus = EmailStatus.SendFailed;
        }
        finally
        {
            await database.SaveAsync(userToken);
        }
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
