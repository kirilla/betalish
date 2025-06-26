using Betalish.Application.Commands.Sessions.SignIn;
using Betalish.Application.Queues.BadSignIns;
using Betalish.Application.Queues.LogItems;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Betalish.Web.Pages.Public.Account;

[AllowAnonymous]
public class SignInModel(
    IUserToken userToken,
    IBadSignInList badSignInList,
    ILogItemList logItemList,
    ISignInCommand signInCommand,
    IOptions<AccountConfiguration> accountOptions) : UserTokenPageModel(userToken)
{
    private readonly AccountConfiguration _config = accountOptions.Value;

    [BindProperty]
    public SignInCommandModel CommandModel { get; set; }

    public IActionResult OnGet()
    {
        try
        {
            if (!_config.SignInAllowed)
                throw new FeatureTurnedOffException();

            if (UserToken.IsAuthenticated)
                throw new AlreadyLoggedInException();

            return Page();
        }
        catch (AlreadyLoggedInException)
        {
            return Redirect("/help/already-logged-in");
        }
        catch (FeatureTurnedOffException)
        {
            return Redirect("/help/featureturnedoff");
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
            if (!_config.SignInAllowed)
                throw new FeatureTurnedOffException();

            if (UserToken.IsAuthenticated)
                throw new AlreadyLoggedInException();

            Thread.Sleep(
                TimeSpan.FromMilliseconds(
                    Random.Shared.Next(200, 600)));

            if (!ModelState.IsValid)
                return Page();

            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

            var loginResult = await signInCommand.Execute(UserToken, CommandModel, ipAddress);

            var claims = new List<Claim>
            {
                new Claim("UserGuid", loginResult.UserGuid.ToString()),
                new Claim("SessionGuid", loginResult.SessionGuid.ToString()),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            logItemList.AddLogItem(new LogItem()
            {
                IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                LogItemKind = LogItemKind.SignIn,
                UserId = loginResult.UserId,
            });

            return Redirect("/show-lobby");
        }
        catch (AlreadyLoggedInException)
        {
            return Redirect("/help/already-logged-in");
        }
        catch (FeatureTurnedOffException)
        {
            return Redirect("/help/featureturnedoff");
        }
        catch (UserNoLoginException)
        {
            return Redirect("/help/user-account-locked");
        }
        catch (Exception ex)
        {
            badSignInList.AddSignIn(
                HttpContext.Connection.RemoteIpAddress,
                CommandModel.EmailAddress,
                CommandModel.Password,
                ex);

            if (ex is UserNotFoundException ||
                ex is PasswordVerificationFailedException)
            {
                // NOTE: Don't let the user/attacker know
                // which of these is the case.

                ModelState.AddModelError(
                    nameof(CommandModel.EmailAddress), 
                    "Inloggningen misslyckades.");

                return Page();
            }

            return Redirect("/help/notpermitted");
        }
    }
}
