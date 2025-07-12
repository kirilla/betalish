namespace Betalish.Application.Commands.Customers.AddCustomerOrganization;

public interface IAddCustomerOrganizationCommand
{
    Task<int> Execute(IUserToken userToken, AddCustomerOrganizationCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
