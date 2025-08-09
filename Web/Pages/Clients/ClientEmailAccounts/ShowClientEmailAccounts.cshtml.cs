namespace Betalish.Web.Pages.Clients.ClientEmailAccounts;

public class ShowClientEmailAccountsModel(
    IUserToken userToken,
    IDatabaseService database) : ClientPageModel(userToken)
{
    public List<ClientEmailAccount> ClientEmailAccounts { get; set; } = null!;

    public bool CanAddClientEmailAccount { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            AssertIsClient();

            ClientEmailAccounts = await database.EmailAccounts
                .AsNoTracking()
                .Where(x => x.ClientId == UserToken.ClientId!.Value)
                .OrderBy(x => x.FromName)
                .ThenBy(x => x.FromAddress)
                .ToListAsync();

            CanAddClientEmailAccount = ClientEmailAccounts.Count == 0;

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
