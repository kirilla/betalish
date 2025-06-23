namespace Betalish.Application.Commands.Customers.AddCustomer;

public interface IAddCustomerCommand
{
    Task<int> Execute(IUserToken userToken, AddCustomerCommandModel model, int clientId);

    Task<bool> IsPermitted(IUserToken userToken, int clientId);
}
