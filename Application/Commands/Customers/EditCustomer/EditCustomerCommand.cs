namespace Betalish.Application.Commands.Customers.EditCustomer;

public class EditCustomerCommand(IDatabaseService database) : IEditCustomerCommand
{
    public async Task Execute(
        IUserToken userToken, EditCustomerCommandModel model)
    {
        if (!await IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var customer = await database.Customers
            .Where(x => 
                x.Id == model.Id &&
                x.ClientId == userToken.ClientId!.Value)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        if (await database.Customers
            .AnyAsync(x =>
                x.Address == model.Address &&
                x.ClientId == userToken.ClientId!.Value &&
                x.Id != model.Id))
            throw new BlockedByAddressException();

        customer.Name = model.Name;
        customer.Address = model.Address;

        await database.SaveAsync(userToken);
    }

    public async Task<bool> IsPermitted(IUserToken userToken)
    {
        return await database.ClientAuths.AnyAsync(x =>
            x.ClientId == userToken.ClientId!.Value &&
            x.UserId == userToken.UserId!.Value);
    }
}
