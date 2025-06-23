namespace Betalish.Application.Commands.Sessions.SelectClient;

public interface ISelectClientCommand
{
    Task Execute(IUserToken userToken, SelectClientCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
