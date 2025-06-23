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
        IUserToken userToken, SendClientEmailCommandModel model, int clientId)
    {
        if (!await IsPermitted(userToken, clientId))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var smtpConfiguration = smtpOptions.Value;

        var message = await database.ClientEmailMessages
            .Where(x => 
                x.Id == model.Id &&
                x.ClientId == clientId)
            .SingleOrDefaultAsync() ??
             throw new NotFoundException();

        try
        {
            smtpService.SendMessage(smtpConfiguration, message);

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

    public async Task<bool> IsPermitted(IUserToken userToken, int clientId)
    {
        return await database.ClientAuths.AnyAsync(x =>
            x.ClientId == clientId &&
            x.UserId == userToken.UserId!.Value);
    }
}
