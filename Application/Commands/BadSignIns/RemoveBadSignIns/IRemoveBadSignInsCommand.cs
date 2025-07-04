namespace Betalish.Application.Commands.BadSignIns.RemoveBadSignIns;

public interface IRemoveBadSignInsCommand
{
    Task Execute(IUserToken userToken, RemoveBadSignInsCommandModel model);

    Task<bool> IsPermitted(IUserToken userToken);
}
