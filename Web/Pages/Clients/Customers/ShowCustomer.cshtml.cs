using Betalish.Application.Commands.Customers.EditCustomer;
using Betalish.Application.Commands.Customers.RemoveCustomer;

namespace Betalish.Web.Pages.Clients.Customers;

public class ShowCustomerModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditCustomerCommand editCustomerCommand,
    IRemoveCustomerCommand removeCustomerCommand) : ClientPageModel(userToken)
{
    public Customer Customer { get; set; }

    public bool CanEditCustomer { get; set; }
    public bool CanRemoveCustomer { get; set; }

    public async Task<IActionResult> OnGetAsync(int clientId, int customerId)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            await AssertClientAuthorization(database, clientId);

            Client = await database.Clients
                .Where(x => x.Id == clientId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            Customer = await database.Customers
                .Where(x => 
                    x.Id == customerId &&
                    x.ClientId == clientId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CanEditCustomer = await editCustomerCommand.IsPermitted(userToken, clientId);
            CanRemoveCustomer = await removeCustomerCommand.IsPermitted(userToken, clientId);

            return Page();
        }
        catch (NotFoundException)
        {
            return Redirect("/help/notfound");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
