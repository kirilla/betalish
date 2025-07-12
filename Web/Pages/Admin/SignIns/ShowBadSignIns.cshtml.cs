namespace Betalish.Web.Pages.Admin.SignIns;

public class ShowBadSignInsModel(
    IUserToken userToken,
    IDatabaseService database) : AdminPageModel(userToken)
{
    public List<BadSignIn> BadSignIns { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            AssertIsAdmin();

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
