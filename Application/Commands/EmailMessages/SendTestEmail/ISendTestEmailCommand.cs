namespace Betalish.Application.Commands.EmailMessages.SendTestEmail;

public interface ISendTestEmailCommand
{
    Task Execute(IUserToken userToken, SendTestEmailCommandModel model);

    Task<bool> IsPermitted(IUserToken userToken);
}
