namespace Betalish.Application.Commands.Users.EditUser;

public interface IEditUserCommand
{
    Task Execute(IUserToken userToken, EditUserCommandModel model);

    Task<bool> IsPermitted(IUserToken userToken);
}
