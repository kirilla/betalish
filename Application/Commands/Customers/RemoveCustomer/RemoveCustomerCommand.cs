namespace Betalish.Application.Commands.Customers.RemoveCustomer;

public class RemoveCustomerCommand(IDatabaseService database) : IRemoveCustomerCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveCustomerCommandModel model, int clientId)
    {
        if (!await IsPermitted(userToken, clientId))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        // TODO: Client authorization

        var customer = await database.Customers
            .Where(x => x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.Customers.Remove(customer);

        await database.SaveAsync(userToken);
    }

    public async Task<bool> IsPermitted(IUserToken userToken, int clientId)
    {
        return await database.ClientAuths.AnyAsync(x =>
            x.ClientId == clientId &&
            x.UserId == userToken.UserId!.Value);
    }
}
