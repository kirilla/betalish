namespace Betalish.Web.Pages.Admin.ClientEvents;

public class ShowClientEventsModel(
    IUserToken userToken,
    IDatabaseService database) : AdminPageModel(userToken)
{
    public List<ClientEvent> ClientEvents { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            await AssertAdminAuthorization(database);

            ClientEvents = await database.ClientEvents
                .AsNoTracking()
                .Include(x => x.Client)
                .Include(x => x.User)
                .OrderByDescending(x => x.Created)
                .ToListAsync();

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
