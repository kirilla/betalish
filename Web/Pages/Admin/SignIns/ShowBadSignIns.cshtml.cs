namespace Betalish.Web.Pages.Admin.SignIns;

public class ShowBadSignInsModel(
    IUserToken userToken,
    IDatabaseService database) : AdminPageModel(userToken)
{
    public List<BadSignIn> BadSignIns { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            await AssertAdminAuthorization(database);

            BadSignIns = await database.BadSignIns
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
