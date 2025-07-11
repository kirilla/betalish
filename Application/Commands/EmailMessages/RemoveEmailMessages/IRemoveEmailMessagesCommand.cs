namespace Betalish.Application.Commands.EmailMessages.RemoveEmailMessages;

public interface IRemoveEmailMessagesCommand
{
    Task Execute(IUserToken userToken, RemoveEmailMessagesCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
