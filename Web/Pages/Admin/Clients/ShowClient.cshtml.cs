using Betalish.Application.Commands.Clients.EditClient;
using Betalish.Application.Commands.Clients.RemoveClient;

namespace Betalish.Web.Pages.Admin.Clients;

public class ShowClientModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditClientCommand editClientCommand,
    IRemoveClientCommand removeClientCommand) : AdminPageModel(userToken)
{
    public Client Client { get; set; }

    public List<User> Users { get; set; }

    public bool CanEditClient { get; set; }
    public bool CanRemoveClient { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            await AssertAdminAuthorization(database);

            Client = await database.Clients
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            Users = await database.ClientAuths
                .Where(x => x.ClientId == id)
                .Select(x => x.User)
                .ToListAsync();

            CanEditClient = await editClientCommand.IsPermitted(userToken);
            CanRemoveClient = await removeClientCommand.IsPermitted(userToken);

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
