using Betalish.Application.Commands.UserEmails.AddUserEmail;

namespace Betalish.Web.Pages.Account;

public class AddUserEmailModel(
    IUserToken userToken,
    IDatabaseService database,
    IAddUserEmailCommand command) : UserTokenPageModel(userToken)
{
    [BindProperty]
    public AddUserEmailCommandModel CommandModel { get; set; }

    public IActionResult OnGet()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            CommandModel = new AddUserEmailCommandModel();

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
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
