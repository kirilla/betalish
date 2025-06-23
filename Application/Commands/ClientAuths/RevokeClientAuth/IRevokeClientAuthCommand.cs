namespace Betalish.Application.Commands.ClientAuths.RevokeClientAuth;

public interface IRevokeClientAuthCommand
{
    Task Execute(IUserToken userToken, RevokeClientAuthCommandModel model);

    Task<bool> IsPermitted(IUserToken userToken);
}
