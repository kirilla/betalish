namespace Betalish.Application.Commands.EmailMessages.SendEmail;

public interface ISendEmailCommand
{
    Task Execute(IUserToken userToken, SendEmailCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
