namespace Betalish.Application.Commands.Customers.EditCustomer;

public class EditCustomerCommand(IDatabaseService database) : IEditCustomerCommand
{
    public async Task Execute(
        IUserToken userToken, EditCustomerCommandModel model, int clientId)
    {
        if (!await IsPermitted(userToken, clientId))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        // TODO: Client authorization

        var customer = await database.Customers
            .Where(x => x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        if (await database.Customers
            .AnyAsync(x =>
                x.Address == model.Address &&
                x.Id != model.Id))
            throw new BlockedByAddressException();

        customer.Name = model.Name;
        customer.Address = model.Address;

        await database.SaveAsync(userToken);
    }

    public async Task<bool> IsPermitted(IUserToken userToken, int clientId)
    {
        return await database.ClientAuths.AnyAsync(x =>
            x.ClientId == clientId &&
            x.UserId == userToken.UserId!.Value);
    }
}
