namespace Betalish.Application.Commands.ClientAuths.RevokeClientAuth;

public interface IRevokeClientAuthCommand
{
    Task Execute(IUserToken userToken, RevokeClientAuthCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
