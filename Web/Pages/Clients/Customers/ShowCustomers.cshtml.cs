using Betalish.Application.Commands.Customers.AddCustomerPerson;

namespace Betalish.Web.Pages.Clients.Customers;

public class ShowCustomersModel(
    IUserToken userToken,
    IDatabaseService database,
    IAddCustomerPersonCommand addCustomerCommand) : ClientPageModel(userToken)
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

            Customers = await database.Customers
                .AsNoTracking()
                .Where(x => x.ClientId == UserToken.ClientId!.Value)
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Ssn10)
                .ThenBy(x => x.Orgnum)
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
