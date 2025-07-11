namespace Betalish.Application.Commands.Sessions.UnselectClient;

public interface IUnselectClientCommand
{
    Task Execute(IUserToken userToken, UnselectClientCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
