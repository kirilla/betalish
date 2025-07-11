namespace Betalish.Application.Commands.Customers.AddCustomerPerson;

public interface IAddCustomerPersonCommand
{
    Task<int> Execute(IUserToken userToken, AddCustomerPersonCommandModel model);

    Task<bool> IsPermitted(IUserToken userToken);
}
