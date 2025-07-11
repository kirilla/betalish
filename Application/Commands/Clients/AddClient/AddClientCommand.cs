namespace Betalish.Application.Commands.Clients.AddClient;

public class AddClientCommand(IDatabaseService database) : IAddClientCommand
{
    public async Task<int> Execute(
        IUserToken userToken, AddClientCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        if (await database.Clients
            .AnyAsync(x => x.Address == model.Address))
            throw new BlockedByAddressException();

        var client = new Client()
        {
            Name = model.Name,
            Address = model.Address,
        };

        database.Clients.Add(client);

        await database.SaveAsync(userToken);

        return client.Id;
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAdmin;
    }
}
