namespace Betalish.Application.Commands.Customers.EditCustomer;

public interface IEditCustomerCommand
{
    Task Execute(IUserToken userToken, EditCustomerCommandModel model);

    Task<bool> IsPermitted(IUserToken userToken);
}
