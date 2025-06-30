namespace Betalish.Web.Pages.Admin.Sessions;

public class ShowSessionRecordsModel(
    IUserToken userToken,
    IDatabaseService database) : AdminPageModel(userToken)
{
    public List<SessionRecord> SessionRecords { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            await AssertAdminAuthorization(database);

            SessionRecords = await database.SessionRecords
                .Include(x => x.Client)
                .Include(x => x.User)
                .OrderBy(x => x.Login)
                .ThenBy(x => x.Logout)
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
