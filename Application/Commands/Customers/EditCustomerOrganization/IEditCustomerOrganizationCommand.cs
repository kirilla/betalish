namespace Betalish.Application.Commands.Customers.EditCustomerOrganization;

public interface IEditCustomerOrganizationCommand
{
    Task Execute(IUserToken userToken, EditCustomerOrganizationCommandModel model);

    Task<bool> IsPermitted(IUserToken userToken);
}
