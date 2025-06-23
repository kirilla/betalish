namespace Betalish.Application.Commands.UserEmails.RemoveUserEmail;

public interface IRemoveUserEmailCommand
{
    Task Execute(IUserToken userToken, RemoveUserEmailCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
