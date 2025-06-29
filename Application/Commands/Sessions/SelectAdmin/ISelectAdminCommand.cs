namespace Betalish.Application.Commands.Sessions.SelectAdmin;

public interface ISelectAdminCommand
{
    Task Execute(IUserToken userToken, SelectAdminCommandModel model);

    Task<bool> IsPermitted(IUserToken userToken);
}
