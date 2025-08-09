namespace Betalish.Web.Pages.Clients.EmailAccounts;

public class ShowEmailAccountsModel(
    IUserToken userToken,
    IDatabaseService database) : ClientPageModel(userToken)
{
    public List<EmailAccount> EmailAccounts { get; set; } = null!;

    public bool CanAddEmailAccount { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            AssertIsClient();

            EmailAccounts = await database.EmailAccounts
                .AsNoTracking()
                .Where(x => x.ClientId == UserToken.ClientId!.Value)
                .OrderBy(x => x.FromName)
                .ThenBy(x => x.FromAddress)
                .ToListAsync();

            CanAddEmailAccount = EmailAccounts.Count == 0;

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
