namespace Betalish.Application.Commands.Batches.RemoveBatch;

public class RemoveBatchCommand(IDatabaseService database) : IRemoveBatchCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveBatchCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var batch = await database.Batches
            .Where(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.Batches.Remove(batch);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
