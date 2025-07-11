namespace Betalish.Application.Commands.Users.StripUserAdmin;

public interface IStripUserAdminCommand
{
    Task Execute(IUserToken userToken, StripUserAdminCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
