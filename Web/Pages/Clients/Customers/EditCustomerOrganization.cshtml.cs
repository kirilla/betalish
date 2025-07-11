using Betalish.Application.Commands.Customers.EditCustomerOrganization;

namespace Betalish.Web.Pages.Clients.Customers;

public class EditCustomerOrganizationModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditCustomerOrganizationCommand command) : ClientPageModel(userToken)
{
    public Customer Customer { get; set; }

    [BindProperty]
    public EditCustomerOrganizationCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!await command.IsPermitted(UserToken))
                throw new NotPermittedException();

            Customer = await database.Customers
                .Where(x =>
                    x.Id == id &&
                    x.ClientId == UserToken.ClientId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new EditCustomerOrganizationCommandModel()
            {
                Id = Customer.Id,
                Name = Customer.Name,
                Orgnum = Customer.Orgnum?.ToOrgnumWithDash(),
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
        catch (BlockedByOrgnumException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Orgnum),
                "Det finns en annan kund eller medlem med detta organisationsnummer.");

            return Page();
        }
        catch (InvalidOrgnumException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Orgnum),
                "Ogiltigt organisationsnummer.");

            return Page();
        }
        catch (Exception ex)
        {
            return Redirect("/help/notpermitted");
        }
    }
}
