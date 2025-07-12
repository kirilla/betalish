namespace Betalish.Application.Commands.ClientEmailMessages.SendClientEmail;

public interface ISendClientEmailCommand
{
    Task Execute(IUserToken userToken, SendClientEmailCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
