using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Betalish.Application.Commands.Sessions.SignOut;

namespace Betalish.Web.Pages.Account;

public class SignOutModel(
    ISignOutCommand command,
    IUserToken userToken) : UserTokenPageModel(userToken)
{
    public IActionResult OnGet()
    {
        try
        {
            if (!User?.Identity?.IsAuthenticated ?? false)
                return Redirect("/");

            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            if (UserToken.IsAuthenticated)
            {
                await command.Execute(UserToken);
            }

            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("/sign-out-success");
        }
        catch
        {
            return Redirect("/help/logout-error");
        }
    }
}
