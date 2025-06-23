using Betalish.Application.Commands.ClientEmailAccounts.EditClientEmailAccount;
using Betalish.Application.Commands.ClientEmailAccounts.RemoveClientEmailAccount;

namespace Betalish.Web.Pages.Clients.ClientEmailAccounts;

public class ShowClientEmailAccountModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditClientEmailAccountCommand editClientEmailAccountCommand,
    IRemoveClientEmailAccountCommand removeClientEmailAccountCommand) : ClientPageModel(userToken)
{
    public ClientEmailAccount ClientEmailAccount { get; set; }

    public bool CanEditClientEmailAccount { get; set; }
    public bool CanRemoveClientEmailAccount { get; set; }

    public async Task<IActionResult> OnGetAsync(int clientId, int clientEmailAccountId)
    {
        try
        {
            await AssertClientAuthorization(database, clientId);

            Client = await database.Clients
                .Where(x => x.Id == clientId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            ClientEmailAccount = await database.ClientEmailAccounts
                .Where(x => 
                    x.Id == clientEmailAccountId &&
                    x.ClientId == clientId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CanEditClientEmailAccount = 
                await editClientEmailAccountCommand.IsPermitted(userToken, clientId);

            CanRemoveClientEmailAccount = 
                await removeClientEmailAccountCommand.IsPermitted(userToken, clientId);

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
