using Betalish.Application.Commands.LedgerAccounts.AddLedgerAccount;

namespace Betalish.Web.Pages.Clients.LedgerAccounts;

public class AddLedgerAccountModel(
    IUserToken userToken,
    IAddLedgerAccountCommand command) : ClientPageModel(userToken)
{
    [BindProperty]
    public AddLedgerAccountCommandModel CommandModel { get; set; }
        = new AddLedgerAccountCommandModel();

    public IActionResult OnGet()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            CommandModel = new AddLedgerAccountCommandModel();

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

            return Redirect($"/show-ledger-accounts");
        }
        catch (BlockedByExistingException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Account),
                "Det finns redan ett konto med samma nummer.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
