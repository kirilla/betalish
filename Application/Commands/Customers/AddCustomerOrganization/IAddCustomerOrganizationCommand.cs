namespace Betalish.Application.Commands.Customers.AddCustomerOrganization;

public interface IAddCustomerOrganizationCommand
{
    Task<int> Execute(IUserToken userToken, AddCustomerOrganizationCommandModel model);

    Task<bool> IsPermitted(IUserToken userToken);
}
