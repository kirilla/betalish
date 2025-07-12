namespace Betalish.Application.Commands.ClientEmailMessages.RemoveClientEmailMessages;

public interface IRemoveClientEmailMessagesCommand
{
    Task Execute(IUserToken userToken, RemoveClientEmailMessagesCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
