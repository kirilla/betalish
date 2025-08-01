namespace Betalish.Application.Commands.Batches.EditBatch;

public interface IEditBatchCommand
{
    Task Execute(IUserToken userToken, EditBatchCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
