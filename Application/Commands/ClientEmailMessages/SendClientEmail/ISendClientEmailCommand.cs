namespace Betalish.Application.Commands.ClientEmailMessages.SendClientEmail;

public interface ISendClientEmailCommand
{
    Task Execute(IUserToken userToken, SendClientEmailCommandModel model, int clientId);

    Task<bool> IsPermitted(IUserToken userToken, int clientId);
}
