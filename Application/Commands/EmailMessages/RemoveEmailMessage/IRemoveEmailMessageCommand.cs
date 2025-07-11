namespace Betalish.Application.Commands.EmailMessages.RemoveEmailMessage;

public interface IRemoveEmailMessageCommand
{
    Task Execute(IUserToken userToken, RemoveEmailMessageCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
