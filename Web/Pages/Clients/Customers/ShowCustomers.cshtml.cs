using Betalish.Application.Commands.Customers.AddCustomer;

namespace Betalish.Web.Pages.Clients.Customers;

public class ShowCustomersModel(
    IUserToken userToken,
    IDatabaseService database,
    IAddCustomerCommand addCustomerCommand) : ClientPageModel(userToken)
{
    public List<Customer> Customers { get; set; }

    public bool CanAddCustomer { get; set; }

    public async Task<IActionResult> OnGetAsync(int clientId)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            await AssertClientAuthorization(database, clientId);

            Client = await database.Clients
                .AsNoTracking()
                .Where(x => x.Id == clientId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            Customers = await database.Customers
                .AsNoTracking()
                .Where(x => x.ClientId == clientId)
                .OrderBy(x => x.Address)
                .ThenBy(x => x.Name)
                .ToListAsync();

            CanAddCustomer = await addCustomerCommand.IsPermitted(userToken, clientId);

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
