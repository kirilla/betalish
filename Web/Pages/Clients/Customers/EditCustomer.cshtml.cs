using Betalish.Application.Commands.Customers.EditCustomer;

namespace Betalish.Web.Pages.Clients.Customers;

public class EditCustomerModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditCustomerCommand command) : ClientPageModel(userToken)
{
    public Customer Customer { get; set; }

    [BindProperty]
    public EditCustomerCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int clientId, int customerId)
    {
        try
        {
            if (!await command.IsPermitted(UserToken, clientId))
                throw new NotPermittedException();

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

            CommandModel = new EditCustomerCommandModel()
            {
                Id = Customer.Id,
                Name = Customer.Name,
                Address = Customer.Address,
            };

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

    public async Task<IActionResult> OnPostAsync(int clientId, int customerId)
    {
        try
        {
            if (!await command.IsPermitted(UserToken, clientId))
                throw new NotPermittedException();

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

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel, clientId);

            return Redirect($"/client/{clientId}/show-customer/{customerId}");
        }
        catch (BlockedByAddressException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Address),
                "Det finns en annan kund med samma adress.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
