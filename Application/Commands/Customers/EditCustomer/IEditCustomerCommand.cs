namespace Betalish.Application.Commands.Customers.EditCustomer;

public interface IEditCustomerCommand
{
    Task Execute(IUserToken userToken, EditCustomerCommandModel model, int clientId);

    Task<bool> IsPermitted(IUserToken userToken, int clientId);
}
