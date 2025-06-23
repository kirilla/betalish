namespace Betalish.Application.Commands.Sessions.EndSession;

public interface IEndSessionCommand
{
    Task Execute(IUserToken userToken, EndSessionCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
