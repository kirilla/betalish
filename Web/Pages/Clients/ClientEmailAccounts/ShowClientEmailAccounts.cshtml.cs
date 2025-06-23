using Betalish.Application.Commands.ClientEmailAccounts.AddClientEmailAccount;

namespace Betalish.Web.Pages.Clients.ClientEmailAccounts;

public class ShowClientEmailAccountsModel(
    IUserToken userToken,
    IDatabaseService database,
    IAddClientEmailAccountCommand addClientEmailAccountCommand) : ClientPageModel(userToken)
{
    public List<ClientEmailAccount> ClientEmailAccounts { get; set; }

    public bool CanAddClientEmailAccount { get; set; }

    public async Task<IActionResult> OnGetAsync(int clientId)
    {
        try
        {
            await AssertClientAuthorization(database, clientId);

            Client = await database.Clients
                .Where(x => x.Id == clientId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            ClientEmailAccounts = await database.ClientEmailAccounts
                .AsNoTracking()
                .Where(x => x.ClientId == clientId)
                .OrderBy(x => x.FromName)
                .ThenBy(x => x.FromAddress)
                .ToListAsync();

            CanAddClientEmailAccount = 
                await addClientEmailAccountCommand.IsPermitted(userToken, clientId);

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
