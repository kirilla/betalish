namespace Betalish.Application.Commands.Clients.EditClient;

public interface IEditClientCommand
{
    Task Execute(IUserToken userToken, EditClientCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
