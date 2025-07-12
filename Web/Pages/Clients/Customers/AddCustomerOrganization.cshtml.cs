using Betalish.Application.Commands.Customers.AddCustomerOrganization;

namespace Betalish.Web.Pages.Clients.Customers;

public class AddCustomerOrganizationModel(
    IUserToken userToken,
    IDatabaseService database,
    IAddCustomerOrganizationCommand command) : ClientPageModel(userToken)
{
    [BindProperty]
    public AddCustomerOrganizationCommandModel CommandModel { get; set; }

    public IActionResult OnGet()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            CommandModel = new AddCustomerOrganizationCommandModel();

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
        catch (BlockedByOrgnumException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Orgnum),
                "Det finns redan en kund eller medlem med detta organisationsnummer.");

            return Page();
        }
        catch (InvalidOrgnumException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Orgnum),
                "Ogiltigt organisationsnummer.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
