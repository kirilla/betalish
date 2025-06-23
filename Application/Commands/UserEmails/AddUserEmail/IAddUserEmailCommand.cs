namespace Betalish.Application.Commands.UserEmails.AddUserEmail;

public interface IAddUserEmailCommand
{
    Task Execute(IUserToken userToken, AddUserEmailCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
