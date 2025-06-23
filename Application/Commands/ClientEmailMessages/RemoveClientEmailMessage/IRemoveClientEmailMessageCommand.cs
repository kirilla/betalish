namespace Betalish.Application.Commands.ClientEmailMessages.RemoveClientEmailMessage;

public interface IRemoveClientEmailMessageCommand
{
    Task Execute(
        IUserToken userToken, 
        RemoveClientEmailMessageCommandModel model, 
        int clientId);

    Task<bool> IsPermitted(IUserToken userToken, int clientId);
}
