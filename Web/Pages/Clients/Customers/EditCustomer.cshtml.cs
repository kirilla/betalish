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

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!await command.IsPermitted(UserToken))
                throw new NotPermittedException();

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

            CommandModel = new EditCustomerCommandModel()
            {
                Id = Customer.Id,
                Name = Customer.Name,
                EmailAddress = Customer.EmailAddress,
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

    public async Task<IActionResult> OnPostAsync(int id)
    {
        try
        {
            if (!await command.IsPermitted(UserToken))
                throw new NotPermittedException();

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

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-customer/{id}");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
