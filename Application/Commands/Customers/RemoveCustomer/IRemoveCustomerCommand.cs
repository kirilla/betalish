namespace Betalish.Application.Commands.Customers.RemoveCustomer;

public interface IRemoveCustomerCommand
{
    Task Execute(IUserToken userToken, RemoveCustomerCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
