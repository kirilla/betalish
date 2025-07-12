namespace Betalish.Application.Commands.ClientEmailMessages.RemoveClientEmailMessage;

public interface IRemoveClientEmailMessageCommand
{
    Task Execute(
        IUserToken userToken, 
        RemoveClientEmailMessageCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
