using Betalish.Application.Commands.Customers.AddCustomerPerson;

namespace Betalish.Web.Pages.Clients.Customers;

public class AddCustomerPersonModel(
    IUserToken userToken,
    IDatabaseService database,
    IAddCustomerPersonCommand command) : ClientPageModel(userToken)
{
    [BindProperty]
    public AddCustomerPersonCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            CommandModel = new AddCustomerPersonCommandModel();

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
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            if (!ModelState.IsValid)
                return Page();

            var id = await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-customer/{id}");
        }
        catch (BlockedBySsnException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Ssn10),
                "Det finns redan en kund eller medlem med detta personnummer.");

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
