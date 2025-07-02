using Microsoft.AspNetCore.Authorization;

namespace Betalish.Web.Pages.Public.Account;

[AllowAnonymous]
public class SignInModel(
    IUserToken userToken,
    IOptions<SignInConfiguration> options) : UserTokenPageModel(userToken)
{
    public readonly SignInConfiguration SignInConfiguration = options.Value;

    public IActionResult OnGet()
    {
        try
        {
            if (UserToken.IsAuthenticated)
                throw new AlreadyLoggedInException();

            return Page();
        }
        catch (AlreadyLoggedInException)
        {
            return Redirect("/help/already-logged-in");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
