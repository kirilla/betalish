using Betalish.Application.Commands.Customers.EditCustomerPerson;

namespace Betalish.Web.Pages.Clients.Customers;

public class EditCustomerPersonModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditCustomerPersonCommand command) : ClientPageModel(userToken)
{
    public Customer Customer { get; set; } = null!;

    [BindProperty]
    public EditCustomerPersonCommandModel CommandModel { get; set; }
        = new EditCustomerPersonCommandModel();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            Customer = await database.Customers
                .Where(x =>
                    x.Id == id &&
                    x.ClientId == UserToken.ClientId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new EditCustomerPersonCommandModel()
            {
                Id = Customer.Id,
                Name = Customer.Name,
                Ssn10 = Customer.Ssn10?.ToSsnWithDash(),
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
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

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
        catch (BlockedBySsnException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Ssn10),
                "Det finns en annan kund eller medlem med detta personnummer.");

            return Page();
        }
        catch (InvalidSsnException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Ssn10),
                "Ogiltigt personnummer.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
