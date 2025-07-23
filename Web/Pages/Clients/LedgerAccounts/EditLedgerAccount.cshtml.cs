using Betalish.Application.Commands.LedgerAccounts.EditLedgerAccount;

namespace Betalish.Web.Pages.Clients.LedgerAccounts;

public class EditLedgerAccountModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditLedgerAccountCommand command) : ClientPageModel(userToken)
{
    public LedgerAccount LedgerAccount { get; set; } = null!;

    [BindProperty]
    public EditLedgerAccountCommandModel CommandModel { get; set; }
        = new EditLedgerAccountCommandModel();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            LedgerAccount = await database.LedgerAccounts
                .Where(x =>
                    x.Id == id &&
                    x.ClientId == UserToken.ClientId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new EditLedgerAccountCommandModel()
            {
                Id = LedgerAccount.Id,
                Account = LedgerAccount.Account,
                Description = LedgerAccount.Description,
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
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            LedgerAccount = await database.LedgerAccounts
                .Where(x =>
                    x.Id == id &&
                    x.ClientId == UserToken.ClientId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect("/show-ledger-accounts");
        }
        catch (BlockedByExistingException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Account),
                "Det finns ett annat konto med samma nummer.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
