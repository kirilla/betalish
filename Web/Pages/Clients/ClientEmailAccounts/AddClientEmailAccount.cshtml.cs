using Betalish.Application.Commands.ClientEmailAccounts.AddClientEmailAccount;

namespace Betalish.Web.Pages.Clients.ClientEmailAccounts;

public class AddClientEmailAccountModel(
    IUserToken userToken,
    IDatabaseService database,
    IAddClientEmailAccountCommand command) : ClientPageModel(userToken)
{
    [BindProperty]
    public AddClientEmailAccountCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!await command.IsPermitted(UserToken))
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
            if (!await command.IsPermitted(UserToken))
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
                "Det finns ett annat konto med samma adress.");

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
