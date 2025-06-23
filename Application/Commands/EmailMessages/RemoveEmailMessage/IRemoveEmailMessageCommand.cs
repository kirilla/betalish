namespace Betalish.Application.Commands.EmailMessages.RemoveEmailMessage;

public interface IRemoveEmailMessageCommand
{
    Task Execute(IUserToken userToken, RemoveEmailMessageCommandModel model);

    Task<bool> IsPermitted(IUserToken userToken);
}
