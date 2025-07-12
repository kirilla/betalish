using Betalish.Application.Commands.Account.DeleteAccount;

namespace Betalish.Web.Pages.Account;

public class DeleteAccountModel(
    IUserToken userToken,
    IDeleteAccountCommand command) : UserTokenPageModel(userToken)
{
    [BindProperty]
    public DeleteAccountCommandModel CommandModel { get; set; } = new DeleteAccountCommandModel();

    public IActionResult OnGet()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            CommandModel = new DeleteAccountCommandModel();

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
            {
                return Page();
            }

            await command.Execute(UserToken, CommandModel);

            return Redirect("/delete-account-success");
        }
        catch (ConfirmationRequiredException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Confirmed),
                "Bekräfta att du verkligen vill ta bort ditt konto.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
