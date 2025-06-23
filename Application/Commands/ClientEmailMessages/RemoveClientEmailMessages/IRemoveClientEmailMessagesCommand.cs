namespace Betalish.Application.Commands.ClientEmailMessages.RemoveClientEmailMessages;

public interface IRemoveClientEmailMessagesCommand
{
    Task Execute(IUserToken userToken, RemoveClientEmailMessagesCommandModel model, int clientId);

    Task<bool> IsPermitted(IUserToken userToken, int clientId);
}
