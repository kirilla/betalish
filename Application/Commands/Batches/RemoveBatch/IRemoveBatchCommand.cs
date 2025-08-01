namespace Betalish.Application.Commands.Batches.RemoveBatch;

public interface IRemoveBatchCommand
{
    Task Execute(IUserToken userToken, RemoveBatchCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
