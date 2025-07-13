namespace Betalish.Web.Pages.Admin.Sessions;

public class ShowSessionRecordModel(
    IUserToken userToken,
    IDatabaseService database) : AdminPageModel(userToken)
{
    public SessionRecord SessionRecord { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            AssertIsAdmin();

            SessionRecord = await database.SessionRecords
                .Include(x => x.Client)
                .Include(x => x.User)
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

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
