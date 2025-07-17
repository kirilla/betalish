namespace Betalish.Web.Pages.Clients.LedgerAccounts;

public class ShowLedgerAccountModel(
    IUserToken userToken,
    IDatabaseService database) : ClientPageModel(userToken)
{
    public LedgerAccount LedgerAccount { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            AssertIsClient();

            LedgerAccount = await database.LedgerAccounts
                .Where(x => 
                    x.Id == id &&
                    x.ClientId == UserToken.ClientId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

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
