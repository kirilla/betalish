namespace Betalish.Application.Commands.ClientAuths.RevokeClientAuth;

public class RevokeClientAuthCommand(IDatabaseService database) : IRevokeClientAuthCommand
{
    public async Task Execute(
        IUserToken userToken, RevokeClientAuthCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        var user = await database.Users
            .Where(x => x.Id == model.UserId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        var client = await database.Clients
            .Where(x => x.Id == model.ClientId!.Value)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        var auth = await database.ClientAuths
            .Where(x => 
                x.UserId == user.Id &&
                x.ClientId == client.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.ClientAuths.Remove(auth);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAdmin;
    }
}
