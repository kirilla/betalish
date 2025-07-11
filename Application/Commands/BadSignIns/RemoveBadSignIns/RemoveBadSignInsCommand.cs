namespace Betalish.Application.Commands.BadSignIns.RemoveBadSignIns;

public class RemoveBadSignInsCommand(IDatabaseService database) : IRemoveBadSignInsCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveBadSignInsCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        await database.BadSignIns.ExecuteDeleteAsync();
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAdmin;
    }
}
