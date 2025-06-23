namespace Betalish.Application.Commands.Sessions.SelectClient;

public class SelectClientCommand(
    IDateService dateService,
    IDatabaseService database) : ISelectClientCommand
{
    public async Task Execute(
        IUserToken userToken, SelectClientCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        var auth = await database.ClientAuths
            .Where(x =>
                x.ClientId == model.Id &&
                x.UserId == userToken.UserId!.Value) 
            .SingleOrDefaultAsync() ??
            throw new NotPermittedException();

        var client = await database.Clients
            .Where(x => x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        var session = await database.Sessions
            .Where(x =>
                x.Id == userToken.SessionId!.Value &&
                x.UserId == userToken.UserId!.Value)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        if (session.ClientId == client.Id)
            return;

        session.ClientId = client.Id;

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
