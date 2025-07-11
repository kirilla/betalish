namespace Betalish.Application.Commands.Users.MakeUserAdmin;

public interface IMakeUserAdminCommand
{
    Task Execute(IUserToken userToken, MakeUserAdminCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
