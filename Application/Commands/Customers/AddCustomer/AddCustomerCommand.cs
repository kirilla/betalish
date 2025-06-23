namespace Betalish.Application.Commands.Customers.AddCustomer;

public class AddCustomerCommand(IDatabaseService database) : IAddCustomerCommand
{
    public async Task<int> Execute(
        IUserToken userToken, AddCustomerCommandModel model)
    {
        if (!await IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        if (await database.Customers
            .AnyAsync(x => 
                x.Address == model.Address &&
                x.ClientId == userToken.ClientId!.Value))
            throw new BlockedByAddressException();

        var customer = new Customer()
        {
            Name = model.Name,
            Address = model.Address,
            ClientId = userToken.ClientId!.Value,
        };

        database.Customers.Add(customer);

        await database.SaveAsync(userToken);

        return customer.Id;
    }

    public async Task<bool> IsPermitted(IUserToken userToken)
    {
        return await database.ClientAuths.AnyAsync(x =>
            x.ClientId == userToken.ClientId!.Value &&
            x.UserId == userToken.UserId!.Value);
    }
}
