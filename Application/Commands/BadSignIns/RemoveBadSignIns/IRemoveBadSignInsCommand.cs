namespace Betalish.Application.Commands.BadSignIns.RemoveBadSignIns;

public interface IRemoveBadSignInsCommand
{
    Task Execute(IUserToken userToken, RemoveBadSignInsCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
