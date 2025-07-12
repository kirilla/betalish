namespace Betalish.Application.Commands.Customers.EditCustomerPerson;

public interface IEditCustomerPersonCommand
{
    Task Execute(IUserToken userToken, EditCustomerPersonCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
