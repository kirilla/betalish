namespace Betalish.Application.Commands.EmailMessages.SendTestEmail;

public interface ISendTestEmailCommand
{
    Task Execute(IUserToken userToken, SendTestEmailCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
