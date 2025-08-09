using Betalish.Application.Commands.EmailAccounts.AddEmailAccount;

namespace Betalish.Web.Pages.Clients.EmailAccounts;

public class AddEmailAccountModel(
    IUserToken userToken,
    IAddEmailAccountCommand command) : ClientPageModel(userToken)
{
    [BindProperty]
    public AddEmailAccountCommandModel CommandModel { get; set; } = new();

    public IActionResult OnGet()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            CommandModel = new AddEmailAccountCommandModel();

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

            return Redirect($"/show-email-account/{id}");
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
