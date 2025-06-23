namespace Betalish.Application.Commands.Sessions.EndSession;

public class EndSessionCommand(
    IDateService dateService,
    IDatabaseService database) : IEndSessionCommand
{
    public async Task Execute(
        IUserToken userToken, EndSessionCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        var session = await database.Sessions
            .Where(x =>
                x.Id == model.Id &&
                x.UserId == userToken.UserId!.Value)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        if (session.UserId != userToken.UserId!.Value)
            throw new NotPermittedException();

        database.Sessions.Remove(session);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
