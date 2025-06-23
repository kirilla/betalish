namespace Betalish.Application.Commands.ClientAuths.GrantClientAuth;

public class GrantClientAuthCommand(IDatabaseService database) : IGrantClientAuthCommand
{
    public async Task Execute(
        IUserToken userToken, GrantClientAuthCommandModel model)
    {
        if (!await IsPermitted(userToken))
            throw new NotPermittedException();

        var user = await database.Users
            .Where(x => x.Id == model.UserId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        var client = await database.Clients
            .Where(x => x.Id == model.ClientId!.Value)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        if (await database.ClientAuths.AnyAsync(x =>
            x.ClientId == model.ClientId!.Value &&
            x.UserId == model.UserId))
            throw new BlockedByExistingException();

        var auth = new ClientAuth()
        {
            UserId = user.Id,
            ClientId = client.Id,
        };

        database.ClientAuths.Add(auth); 

        await database.SaveAsync(userToken);
    }

    public async Task<bool> IsPermitted(IUserToken userToken)
    {
        return await database.AdminAuths.AnyAsync(x =>
            x.UserId == userToken.UserId!.Value);
    }
}
