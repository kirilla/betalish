namespace Betalish.Application.Commands.LogItems.RemoveLogItems;

public class RemoveLogItemsCommand(IDatabaseService database) : IRemoveLogItemsCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveLogItemsCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        await database.LogItems.ExecuteDeleteAsync();
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAdmin;
    }
}
