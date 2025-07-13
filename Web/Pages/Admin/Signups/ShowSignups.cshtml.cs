namespace Betalish.Web.Pages.Admin.Signups;

public class ShowSignupsModel(
    IUserToken userToken,
    IDatabaseService database) : AdminPageModel(userToken)
{
    public List<Signup> Signups { get; set; } = [];

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            AssertIsAdmin();

            Signups = await database.Signups
                .OrderBy(x => x.Created)
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
