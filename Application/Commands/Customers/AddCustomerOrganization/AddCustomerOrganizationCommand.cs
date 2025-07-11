namespace Betalish.Application.Commands.Customers.AddCustomerOrganization;

public class AddCustomerOrganizationCommand(IDatabaseService database) : IAddCustomerOrganizationCommand
{
    public async Task<int> Execute(
        IUserToken userToken, AddCustomerOrganizationCommandModel model)
    {
        if (!await IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        model.Orgnum = model.Orgnum?.StripNonNumeric();
        model.EmailAddress = model.EmailAddress?.Trim().ToLowerInvariant();

        model.Orgnum?.AssertOrgnumValid();

        var guid = Guid.NewGuid();

        if (await database.Customers
            .AnyAsync(x => x.Guid == guid))
            throw new BlockedByGuidException();

        if (model.Orgnum.HasValue() &&
            await database.Customers
            .AnyAsync(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.Orgnum == model.Orgnum))
            throw new BlockedByOrgnumException();

        var customer = new Customer()
        {
            Orgnum = model.Orgnum,
            Name = model.Name,
            EmailAddress = model.EmailAddress,
            Guid = guid,
            CustomerKind = CustomerKind.Organization,
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
