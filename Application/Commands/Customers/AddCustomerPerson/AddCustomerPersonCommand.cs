namespace Betalish.Application.Commands.Customers.AddCustomerPerson;

public class AddCustomerPersonCommand(IDatabaseService database) : IAddCustomerPersonCommand
{
    public async Task<int> Execute(
        IUserToken userToken, AddCustomerPersonCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        model.Ssn10 = model.Ssn10?.StripNonNumeric();
        model.EmailAddress = model.EmailAddress?.Trim().ToLowerInvariant();

        model.Ssn10?.AssertSsn10Valid();

        var guid = Guid.NewGuid();

        if (await database.Customers
            .AnyAsync(x => x.Guid == guid))
            throw new BlockedByGuidException();

        if (model.Ssn10.HasValue() &&
            await database.Customers
            .AnyAsync(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.Ssn10 == model.Ssn10))
            throw new BlockedBySsnException();

        var customer = new Customer()
        {
            Ssn10 = model.Ssn10,
            Name = model.Name,
            EmailAddress = model.EmailAddress,
            Guid = guid,
            CustomerKind = CustomerKind.Person,
            ClientId = userToken.ClientId!.Value,
        };

        database.Customers.Add(customer);

        await database.SaveAsync(userToken);

        return customer.Id;
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
