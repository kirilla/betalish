namespace Betalish.Application.Commands.CustomerTags.EditCustomerTags;

public class EditCustomerTagsCommand(IDatabaseService database) : IEditCustomerTagsCommand
{
    public async Task Execute(
        IUserToken userToken, EditCustomerTagsCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var customer = await database.Customers
            .Where(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        var tagsToRemove = await database.CustomerTags
            .Where(x =>
                x.Customer.ClientId == userToken.ClientId!.Value &&
                x.CustomerId == customer.Id)
            .ToListAsync();

        database.CustomerTags.RemoveRange(tagsToRemove);

        var tags = model.Tags?
             .Split(',')
             .Select(x => x.Trim())
             .Select(x => x.TrimEnd('.'))
             .Where(x => !string.IsNullOrWhiteSpace(x))
             .Distinct()
             .ToList() ?? [];

        if (tags.Count > Limits.Customer.Tag.Max)
            throw new TooManyException();

        var tagsToAdd = tags
            .Select(x => new CustomerTag()
            {
                Key = x.Split(":").First(),
                Value = x.Split(':').ElementAtOrDefault(1),
                CustomerId = customer.Id,
            })
            .ToList();

        if (tagsToAdd.Any(x => 
            x.Key.Length > MaxLengths.Common.Tag.Key ||
            x.Value?.Length > MaxLengths.Common.Tag.Value))
            throw new TooLongException();

        database.CustomerTags.AddRange(tagsToAdd);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
