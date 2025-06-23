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

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            await AssertClientAuthorization(database);

            Client = await database.Clients
                .Where(x => x.Id == UserToken.ClientId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            Customer = await database.Customers
                .Where(x => 
                    x.Id == id &&
                    x.ClientId == UserToken.ClientId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CanEditCustomer = await editCustomerCommand.IsPermitted(userToken);
            CanRemoveCustomer = await removeCustomerCommand.IsPermitted(userToken);

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
