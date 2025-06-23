namespace Betalish.Application.Commands.Sessions.SignOut;

public class SignOutCommand(
    IDateService dateService,
    IDatabaseService database) : ISignOutCommand
{
    public async Task Execute(IUserToken userToken)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        var session = await database.Sessions
            .Where(x =>
                x.Id == userToken.SessionId!.Value &&
                x.UserId == userToken.UserId!.Value)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.Sessions.Remove(session);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
