namespace Betalish.Application.Commands.ClientAuths.GrantClientAuth;

public interface IGrantClientAuthCommand
{
    Task Execute(IUserToken userToken, GrantClientAuthCommandModel model);

    Task<bool> IsPermitted(IUserToken userToken);
}
