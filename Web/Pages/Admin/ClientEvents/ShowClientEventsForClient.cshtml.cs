namespace Betalish.Web.Pages.Admin.ClientEvents;

public class ShowClientEventsForClientModel(
    IUserToken userToken,
    IDatabaseService database) : AdminPageModel(userToken)
{
    public Client Client { get; set; } = null!;

    public List<ClientEvent> ClientEvents { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            AssertIsAdmin();

            Client = await database.Clients
                .AsNoTracking()
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ?? 
                throw new NotFoundException();

            ClientEvents = await database.ClientEvents
                .AsNoTracking()
                .Include(x => x.Client)
                .Include(x => x.User)
                .Where(x => x.UserId == id)
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
