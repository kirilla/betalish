namespace Betalish.Application.Commands.Customers.AddCustomer;

public interface IAddCustomerCommand
{
    Task<int> Execute(IUserToken userToken, AddCustomerCommandModel model);

    Task<bool> IsPermitted(IUserToken userToken);
}
