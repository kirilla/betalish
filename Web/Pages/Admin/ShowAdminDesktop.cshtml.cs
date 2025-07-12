namespace Betalish.Web.Pages.Admin;

public class ShowAdminDesktopModel(IUserToken userToken) : AdminPageModel(userToken)
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
