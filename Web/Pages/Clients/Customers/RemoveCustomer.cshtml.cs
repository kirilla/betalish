using Betalish.Application.Commands.Customers.RemoveCustomer;

namespace Betalish.Web.Pages.Clients.Customers;

public class RemoveCustomerModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemoveCustomerCommand command) : ClientPageModel(userToken)
{
    public Customer Customer { get; set; }

    [BindProperty]
    public RemoveCustomerCommandModel CommandModel { get; set; }

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
                .Where(x => x.Id == customerId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new RemoveCustomerCommandModel()
            {
                Id = Customer.Id,
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
                .Where(x => x.Id == customerId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel, clientId);

            return Redirect($"/client/{clientId}/show-customers");
        }
        catch (ConfirmationRequiredException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Confirmed),
                "Bekräfta att du verkligen vill ta bort.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
