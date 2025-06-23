namespace Betalish.Application.Commands.Clients.EditClient;

public class EditClientCommand(IDatabaseService database) : IEditClientCommand
{
    public async Task Execute(
        IUserToken userToken, EditClientCommandModel model)
    {
        if (!await IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var client = await database.Clients
            .Where(x => x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        if (await database.Clients
            .AnyAsync(x =>
                x.Address == model.Address &&
                x.Id != model.Id))
            throw new BlockedByAddressException();

        client.Name = model.Name;
        client.Address = model.Address;

        await database.SaveAsync(userToken);
    }

    public async Task<bool> IsPermitted(IUserToken userToken)
    {
        return await database.AdminAuths.AnyAsync(x =>
            x.UserId == userToken.UserId!.Value);
    }
}
