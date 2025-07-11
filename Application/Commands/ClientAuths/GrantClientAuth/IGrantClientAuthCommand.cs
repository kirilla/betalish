namespace Betalish.Application.Commands.ClientAuths.GrantClientAuth;

public interface IGrantClientAuthCommand
{
    Task Execute(IUserToken userToken, GrantClientAuthCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
