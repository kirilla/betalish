namespace Betalish.Application.Commands.Users.DeleteUser;

public interface IDeleteUserCommand
{
    Task Execute(IUserToken userToken, DeleteUserCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
