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

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            await AssertClientAuthorization(database);

            ClientEmailAccount = await database.ClientEmailAccounts
                .Where(x => 
                    x.Id == id &&
                    x.ClientId == UserToken.ClientId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CanEditClientEmailAccount = 
                await editClientEmailAccountCommand.IsPermitted(userToken);

            CanRemoveClientEmailAccount = 
                await removeClientEmailAccountCommand.IsPermitted(userToken);

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
