using Betalish.Application.Commands.Clients.AddClient;

namespace Betalish.Web.Pages.Admin.Clients;

public class ShowClientsModel(
    IUserToken userToken,
    IDatabaseService database,
    IAddClientCommand addClientCommand) : AdminPageModel(userToken)
{
    public List<Client> Clients { get; set; }

    public bool CanAddClient { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            await AssertAdminAuthorization(database);

            Clients = await database.Clients
                .AsNoTracking()
                .OrderBy(x => x.Address)
                .ThenBy(x => x.Name)
                .ToListAsync();

            CanAddClient = await addClientCommand.IsPermitted(userToken);

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
