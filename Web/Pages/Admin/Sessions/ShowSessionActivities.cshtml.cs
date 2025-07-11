using Betalish.Persistence.Migrations;

namespace Betalish.Web.Pages.Admin.Sessions;

public class ShowSessionActivitiesModel(
    IUserToken userToken,
    IDatabaseService database) : AdminPageModel(userToken)
{
    public List<SessionActivity> SessionActivities { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            AssertIsAdmin();

            SessionActivities = await database.SessionActivities
                .Include(x => x.Session.User)
                .OrderByDescending(x => x.Created)
                .ToListAsync();

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
