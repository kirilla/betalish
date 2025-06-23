namespace Betalish.Application.Commands.Customers.RemoveCustomer;

public interface IRemoveCustomerCommand
{
    Task Execute(IUserToken userToken, RemoveCustomerCommandModel model);

    Task<bool> IsPermitted(IUserToken userToken);
}
