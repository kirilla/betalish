namespace Betalish.Application.Commands.Batches.AddBatch;

public interface IAddBatchCommand
{
    Task<int> Execute(IUserToken userToken, AddBatchCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
