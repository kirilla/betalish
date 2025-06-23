namespace Betalish.Application.Commands.Customers.AddCustomer;

public class AddCustomerCommand(IDatabaseService database) : IAddCustomerCommand
{
    public async Task<int> Execute(
        IUserToken userToken, AddCustomerCommandModel model, int clientId)
    {
        if (!await IsPermitted(userToken, clientId))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        // TODO: Client authorization

        if (await database.Customers
            .AnyAsync(x => x.Address == model.Address))
            throw new BlockedByAddressException();

        var customer = new Customer()
        {
            Name = model.Name,
            Address = model.Address,
            ClientId = model.ClientId,
        };

        database.Customers.Add(customer);

        await database.SaveAsync(userToken);

        return customer.Id;
    }

    public async Task<bool> IsPermitted(IUserToken userToken, int clientId)
    {
        return await database.ClientAuths.AnyAsync(x =>
            x.ClientId == clientId &&
            x.UserId == userToken.UserId!.Value);
    }
}
