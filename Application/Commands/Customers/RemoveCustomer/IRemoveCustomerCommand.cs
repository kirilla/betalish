namespace Betalish.Application.Commands.Customers.RemoveCustomer;

public interface IRemoveCustomerCommand
{
    Task Execute(IUserToken userToken, RemoveCustomerCommandModel model, int clientId);

    Task<bool> IsPermitted(IUserToken userToken, int clientId);
}
