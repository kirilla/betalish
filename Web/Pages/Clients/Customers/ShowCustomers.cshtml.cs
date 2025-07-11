using Betalish.Application.Commands.Customers.AddCustomer;

namespace Betalish.Web.Pages.Clients.Customers;

public class ShowCustomersModel(
    IUserToken userToken,
    IDatabaseService database,
    IAddCustomerCommand addCustomerCommand) : ClientPageModel(userToken)
{
    public List<Customer> Customers { get; set; }

    public bool CanAddCustomer { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            await AssertClientAuthorization(database);

            Client = await database.Clients
                .AsNoTracking()
                .Where(x => x.Id == UserToken.ClientId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            Customers = await database.Customers
                .AsNoTracking()
                .Where(x => x.ClientId == UserToken.ClientId!.Value)
                .OrderBy(x => x.EmailAddress)
                .ThenBy(x => x.Name)
                .ToListAsync();

            CanAddCustomer = await addCustomerCommand.IsPermitted(userToken);

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
