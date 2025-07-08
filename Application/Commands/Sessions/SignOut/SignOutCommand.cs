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

        var record = new SessionRecord()
        {
            UserId = session.UserId,
            Login = session.Created!.Value,
            Logout = dateService.GetDateTimeNow(),
            IpAddress = session.IpAddress,
            ClientId = session.ClientId,
            SignInBy = session.SignInBy,
            SessionEnd = SessionEnd.UserSignOut,
        };

        database.SessionRecords.Add(record);

        database.Sessions.Remove(session);

        var userEvent = new UserEvent()
        {
            UserId = session.UserId,
            UserEventKind = UserEventKind.SignOut,
            IpAddress = session.IpAddress,
        };

        database.UserEvents.Add(userEvent);

        userToken.NoSave = false;

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
