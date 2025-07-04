namespace Betalish.Application.Commands.Sessions.UnselectClient;

public class UnselectClientCommand(
    IDateService dateService,
    IDatabaseService database) : IUnselectClientCommand
{
    public async Task Execute(
        IUserToken userToken, UnselectClientCommandModel model)
    {
        if (!await IsPermitted(userToken))
            throw new NotPermittedException();

        var session = await database.Sessions
            .Where(x =>
                x.Id == userToken.SessionId!.Value &&
                x.UserId == userToken.UserId!.Value)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        if (session.ClientId == null)
            return;

        session.ClientId = null;

        await database.SaveAsync(userToken);
    }

    public async Task<bool> IsPermitted(IUserToken userToken)
    {
        return await database.AdminAuths.AnyAsync(x =>
            x.UserId == userToken.UserId!.Value);
    }
}
