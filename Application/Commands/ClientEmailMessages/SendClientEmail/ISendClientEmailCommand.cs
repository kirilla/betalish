namespace Betalish.Application.Commands.ClientEmailMessages.SendClientEmail;

public interface ISendClientEmailCommand
{
    Task Execute(IUserToken userToken, SendClientEmailCommandModel model);

    Task<bool> IsPermitted(IUserToken userToken);
}
