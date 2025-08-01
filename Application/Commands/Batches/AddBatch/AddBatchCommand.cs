namespace Betalish.Application.Commands.Batches.AddBatch;

public class AddBatchCommand(IDatabaseService database) : IAddBatchCommand
{
    public async Task<int> Execute(
        IUserToken userToken, AddBatchCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        if (await database.Batches
            .AnyAsync(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.Name == model.Name))
            throw new BlockedByExistingException();

        var batch = new Batch()
        {
            Name = model.Name!,
            ClientId = userToken.ClientId!.Value,
        };

        database.Batches.Add(batch);

        await database.SaveAsync(userToken);

        return batch.Id;
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
