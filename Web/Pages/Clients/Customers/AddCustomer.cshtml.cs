using Betalish.Application.Commands.Customers.AddCustomer;

namespace Betalish.Web.Pages.Clients.Customers;

public class AddCustomerModel(
    IUserToken userToken,
    IDatabaseService database,
    IAddCustomerCommand command) : ClientPageModel(userToken)
{
    [BindProperty]
    public AddCustomerCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!await command.IsPermitted(UserToken))
                throw new NotPermittedException();

            Client = await database.Clients
                .Where(x => x.Id == UserToken.ClientId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new AddCustomerCommandModel();

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            if (!await command.IsPermitted(UserToken))
                throw new NotPermittedException();

            Client = await database.Clients
                .Where(x => x.Id == UserToken.ClientId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            var id = await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-customer/{id}");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
