namespace Betalish.Web.Pages.Admin;

public class ShowAdminDesktopModel(
    IUserToken userToken,
    IDatabaseService database) : AdminPageModel(userToken)
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
