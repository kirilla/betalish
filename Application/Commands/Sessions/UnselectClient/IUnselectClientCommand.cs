namespace Betalish.Application.Commands.Sessions.UnselectClient;

public interface IUnselectClientCommand
{
    Task Execute(IUserToken userToken, UnselectClientCommandModel model);

    Task<bool> IsPermitted(IUserToken userToken);
}
