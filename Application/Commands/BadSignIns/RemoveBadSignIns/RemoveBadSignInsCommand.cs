namespace Betalish.Application.Commands.BadSignIns.RemoveBadSignIns;

public class RemoveBadSignInsCommand(IDatabaseService database) : IRemoveBadSignInsCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveBadSignInsCommandModel model)
    {
        if (!await IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        await database.BadSignIns.ExecuteDeleteAsync();
    }

    public async Task<bool> IsPermitted(IUserToken userToken)
    {
        return await database.AdminAuths.AnyAsync(x =>
            x.UserId == userToken.UserId!.Value);
    }
}
