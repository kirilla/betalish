namespace Betalish.Web.Pages.Admin.UserEvents;

public class ShowUserEventsModel(
    IUserToken userToken,
    IDatabaseService database) : AdminPageModel(userToken)
{
    public List<UserEvent> UserEvents { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            await AssertAdminAuthorization(database);

            UserEvents = await database.UserEvents
                .AsNoTracking()
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
