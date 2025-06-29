namespace Betalish.Application.Commands.Sessions.SelectAdmin;

public class SelectAdminCommand(
    IDateService dateService,
    IDatabaseService database) : ISelectAdminCommand
{
    public async Task Execute(
        IUserToken userToken, SelectAdminCommandModel model)
    {
        if (!await IsPermitted(userToken))
            throw new NotPermittedException();

        var session = await database.Sessions
            .Where(x =>
                x.Id == userToken.SessionId!.Value &&
                x.UserId == userToken.UserId!.Value)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        session.ClientId = null;

        await database.SaveAsync(userToken);
    }

    public async Task<bool> IsPermitted(IUserToken userToken)
    {
        return await database.AdminAuths.AnyAsync(x =>
            x.UserId == userToken.UserId!.Value);
    }
}
