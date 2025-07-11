namespace Betalish.Application.Commands.NetworkRequests.RemoveNetworkRequest;

public class RemoveNetworkRequestCommand(IDatabaseService database) : IRemoveNetworkRequestCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveNetworkRequestCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var evt = await database.NetworkRequests
            .Where(x => x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.NetworkRequests.Remove(evt);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAdmin;
    }
}
