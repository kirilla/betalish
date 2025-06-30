namespace Betalish.Application.Commands.UserEmails.AddAccountEmail;

public interface IAddAccountEmailCommand
{
    Task Execute(IUserToken userToken, AddAccountEmailCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
