using Betalish.Application.Commands.UserEmails.AddAccountEmail;

namespace Betalish.Web.Pages.Account;

public class AddAccountEmailModel(
    IUserToken userToken,
    IDatabaseService database,
    IAddAccountEmailCommand command) : UserTokenPageModel(userToken)
{
    [BindProperty]
    public AddAccountEmailCommandModel CommandModel { get; set; } 
        = new AddAccountEmailCommandModel();

    public IActionResult OnGet()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            CommandModel = new AddAccountEmailCommandModel();

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

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-account");
        }
        catch (BlockedByExistingException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Address),
                "Adressen finns redan.");

            return Page();
        }
        catch (TooManyException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Address),
                $"Max {Limits.User.EmailAddresses.Max} adresser.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
