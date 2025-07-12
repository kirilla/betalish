namespace Betalish.Web.Pages.Admin.Tests;

public class ShowTestsModel(IUserToken userToken) : AdminPageModel(userToken)
{
    public IActionResult OnGet()
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
