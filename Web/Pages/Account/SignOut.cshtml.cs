using Betalish.Application.Commands.Sessions.SignOut;
using Betalish.Application.Queues.LogItems;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Betalish.Web.Pages.Account;

public class SignOutModel(
    IUserToken userToken,
    ILogItemList logItemList,
    ISignOutCommand command) : UserTokenPageModel(userToken)
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

            logItemList.AddLogItem(new LogItem()
            {
                IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                LogItemKind = LogItemKind.SignOut,
                UserId = UserToken.UserId,
            });

            return Redirect("/sign-out-success");
        }
        catch
        {
            return Redirect("/help/logout-error");
        }
    }
}
