using Betalish.Application.Commands.Customers.AddCustomer;

namespace Betalish.Web.Pages.Clients.Customers;

public class AddCustomerModel(
    IUserToken userToken,
    IDatabaseService database,
    IAddCustomerCommand command) : ClientPageModel(userToken)
{
    [BindProperty]
    public AddCustomerCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int clientId)
    {
        try
        {
            if (!await command.IsPermitted(UserToken, clientId))
                throw new NotPermittedException();

            Client = await database.Clients
                .Where(x => x.Id == clientId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new AddCustomerCommandModel()
            {
                ClientId = clientId
            };

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }

    public async Task<IActionResult> OnPostAsync(int clientId)
    {
        try
        {
            if (!await command.IsPermitted(UserToken, clientId))
                throw new NotPermittedException();

            Client = await database.Clients
                .Where(x => x.Id == clientId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            var id = await command.Execute(UserToken, CommandModel, CommandModel.ClientId);

            return Redirect($"/client/{clientId}/show-customer/{id}");
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
