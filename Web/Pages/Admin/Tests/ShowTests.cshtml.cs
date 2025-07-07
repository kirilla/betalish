namespace Betalish.Web.Pages.Admin.Tests;

public class ShowTestsModel(
    IDatabaseService database,
    IUserToken userToken) : AdminPageModel(userToken)
{
    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            await AssertAdminAuthorization(database);

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
