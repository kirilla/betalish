namespace Betalish.Application.Commands.Customers.RemoveCustomer;

public class RemoveCustomerCommand(IDatabaseService database) : IRemoveCustomerCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveCustomerCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var customer = await database.Customers
            .Where(x => 
                x.Id == model.Id &&
                x.ClientId == userToken.ClientId!.Value)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.Customers.Remove(customer);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
