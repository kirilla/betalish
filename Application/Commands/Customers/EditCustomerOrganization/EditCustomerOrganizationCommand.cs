namespace Betalish.Application.Commands.Customers.EditCustomerOrganization;

public class EditCustomerOrganizationCommand(IDatabaseService database) : IEditCustomerOrganizationCommand
{
    public async Task Execute(
        IUserToken userToken, EditCustomerOrganizationCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        model.Orgnum = model.Orgnum?.StripNonNumeric();
        model.EmailAddress = model.EmailAddress?.Trim().ToLowerInvariant();

        model.Orgnum?.AssertOrgnumValid();

        var customer = await database.Customers
            .Where(x => 
                x.Id == model.Id &&
                x.ClientId == userToken.ClientId!.Value)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        if (model.Orgnum.HasValue() &&
            await database.Customers
            .AnyAsync(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.Orgnum == model.Orgnum &&
                x.Id != model.Id))
            throw new BlockedByOrgnumException();

        customer.Orgnum = model.Orgnum;
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
