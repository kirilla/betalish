using Betalish.Application.Commands.ClientEmailAccounts.AddClientEmailAccount;

namespace Betalish.Web.Pages.Clients.ClientEmailAccounts;

public class AddClientEmailAccountModel(
    IUserToken userToken,
    IAddClientEmailAccountCommand command) : ClientPageModel(userToken)
{
    [BindProperty]
    public AddClientEmailAccountCommandModel CommandModel { get; set; }
        = new AddClientEmailAccountCommandModel();

    public IActionResult OnGet()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            CommandModel = new AddClientEmailAccountCommandModel();

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

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            if (!ModelState.IsValid)
                return Page();

            var id = await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-client-email-account/{id}");
        }
        catch (BlockedByExistingException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.FromAddress),
                "Det finns redan ett epostkonto.");

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
}
