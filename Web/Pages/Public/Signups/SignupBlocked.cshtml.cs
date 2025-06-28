using Microsoft.AspNetCore.Authorization;

namespace Betalish.Web.Pages.Public.Signups;

[AllowAnonymous]
public class SignupBlockedModel(IUserToken userToken) : UserTokenPageModel(userToken)
{
    public void OnGet()
    {
    }
}
