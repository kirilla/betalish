namespace Betalish.Application.Commands.Sessions.UnselectClient;

public class UnselectClientCommand(
    IDateService dateService,
    IDatabaseService database) : IUnselectClientCommand
{
    public async Task Execute(
        IUserToken userToken, UnselectClientCommandModel model)
    {
        if (!IsPermitted(userToken))
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

        var userEvent = new UserEvent()
        {
            UserId = session.UserId,
            UserEventKind = UserEventKind.UnselectClient,
            IpAddress = session.IpAddress,
        };

        database.UserEvents.Add(userEvent);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAdmin;
    }
}
