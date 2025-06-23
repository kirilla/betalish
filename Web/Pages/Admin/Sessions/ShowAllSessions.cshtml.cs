namespace Betalish.Web.Pages.Admin.Sessions;

public class ShowAllSessionsModel(
    IUserToken userToken,
    IDatabaseService database) : AdminPageModel(userToken)
{
    public List<Session> Sessions { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            await AssertAdminAuthorization(database);

            Sessions = await database.Sessions
                .Include(x => x.User)
                .OrderBy(x => x.User.Name)
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
