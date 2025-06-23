using Microsoft.AspNetCore.Authorization;

namespace Betalish.Web.Pages.Public;

[AllowAnonymous]
public class IndexModel(IUserToken userToken) : UserTokenPageModel(userToken)
{
    public IActionResult OnGet()
    {
        try
        {
            //if (UserToken.IsAuthenticated)
            //    return Redirect("/lobby");

            return Redirect("/frontpage");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
