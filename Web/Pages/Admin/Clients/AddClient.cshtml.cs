using Betalish.Application.Commands.Clients.AddClient;

namespace Betalish.Web.Pages.Admin.Clients;

public class AddClientModel(
    IUserToken userToken,
    IDatabaseService database,
    IAddClientCommand command) : AdminPageModel(userToken)
{
    [BindProperty]
    public AddClientCommandModel CommandModel { get; set; } = new AddClientCommandModel();

    public IActionResult OnGet()
    {
        try
        {
            AssertIsAdmin();

            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            CommandModel = new AddClientCommandModel();

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
            AssertIsAdmin();

            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            if (!ModelState.IsValid)
                return Page();

            var id = await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-client/{id}");
        }
        catch (BlockedByAddressException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Address),
                "Det finns en annan klient med samma adress.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
