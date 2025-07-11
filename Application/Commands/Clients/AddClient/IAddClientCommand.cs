namespace Betalish.Application.Commands.Clients.AddClient;

public interface IAddClientCommand
{
    Task<int> Execute(IUserToken userToken, AddClientCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
