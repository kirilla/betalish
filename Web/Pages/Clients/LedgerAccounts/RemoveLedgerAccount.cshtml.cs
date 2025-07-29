using Betalish.Application.Commands.LedgerAccounts.RemoveLedgerAccount;

namespace Betalish.Web.Pages.Clients.LedgerAccounts;

public class RemoveLedgerAccountModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemoveLedgerAccountCommand command) : ClientPageModel(userToken)
{
    public LedgerAccount LedgerAccount { get; set; } = null!;

    [BindProperty]
    public RemoveLedgerAccountCommandModel CommandModel { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            LedgerAccount = await database.LedgerAccounts
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new RemoveLedgerAccountCommandModel()
            {
                Id = LedgerAccount.Id,
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
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-ledger-accounts");
        }
        catch (ConfirmationRequiredException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Confirmed),
                "Bekräfta att du verkligen vill ta bort.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
