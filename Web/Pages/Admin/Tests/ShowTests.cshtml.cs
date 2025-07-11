namespace Betalish.Web.Pages.Admin.Tests;

public class ShowTestsModel(
    IDatabaseService database,
    IUserToken userToken) : AdminPageModel(userToken)
{
    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            AssertIsAdmin();

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
