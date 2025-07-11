using Betalish.Application.Commands.ClientEmailAccounts.AddClientEmailAccount;

namespace Betalish.Web.Pages.Clients.ClientEmailAccounts;

public class ShowClientEmailAccountsModel(
    IUserToken userToken,
    IDatabaseService database,
    IAddClientEmailAccountCommand addClientEmailAccountCommand) : ClientPageModel(userToken)
{
    public List<ClientEmailAccount> ClientEmailAccounts { get; set; }

    public bool CanAddClientEmailAccount { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            await AssertClientAuthorization(database);

            ClientEmailAccounts = await database.ClientEmailAccounts
                .AsNoTracking()
                .Where(x => x.ClientId == UserToken.ClientId!.Value)
                .OrderBy(x => x.FromName)
                .ThenBy(x => x.FromAddress)
                .ToListAsync();

            CanAddClientEmailAccount = 
                await addClientEmailAccountCommand.IsPermitted(userToken);

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
