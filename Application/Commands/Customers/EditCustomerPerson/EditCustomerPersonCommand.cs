namespace Betalish.Application.Commands.Customers.EditCustomerPerson;

public class EditCustomerPersonCommand(IDatabaseService database) : IEditCustomerPersonCommand
{
    public async Task Execute(
        IUserToken userToken, EditCustomerPersonCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        model.Ssn10 = model.Ssn10?.StripNonNumeric();
        model.EmailAddress = model.EmailAddress?.Trim().ToLowerInvariant();

        model.Ssn10?.AssertSsn10Valid();

        var customer = await database.Customers
            .Where(x => 
                x.Id == model.Id &&
                x.ClientId == userToken.ClientId!.Value)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        if (model.Ssn10.HasValue() &&
            await database.Customers
            .AnyAsync(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.Ssn10 == model.Ssn10 &&
                x.Id != model.Id))
            throw new BlockedBySsnException();

        customer.Ssn10 = model.Ssn10;
        customer.Name = model.Name;

        customer.Address1 = model.Address1;
        customer.Address2 = model.Address2;
        customer.ZipCode = model.ZipCode!;
        customer.City = model.City!;
        customer.Country = model.Country;

        customer.EmailAddress = model.EmailAddress;

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
