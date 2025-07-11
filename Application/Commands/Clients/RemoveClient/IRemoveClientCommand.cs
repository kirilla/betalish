namespace Betalish.Application.Commands.Clients.RemoveClient;

public interface IRemoveClientCommand
{
    Task Execute(IUserToken userToken, RemoveClientCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
