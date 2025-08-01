namespace Betalish.Application.Commands.Batches.EditBatch;

public class EditBatchCommand(IDatabaseService database) : IEditBatchCommand
{
    public async Task Execute(
        IUserToken userToken, EditBatchCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        if (await database.Batches
            .AnyAsync(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.Name == model.Name &&
                x.Id != model.Id))
            throw new BlockedByExistingException();

        var batch = await database.Batches
            .Where(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        batch.Name = model.Name!;

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
