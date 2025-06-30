namespace Betalish.Application.Commands.UserEmails.RemoveAccountEmail;

public interface IRemoveAccountEmailCommand
{
    Task Execute(IUserToken userToken, RemoveAccountEmailCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
