using Microsoft.AspNetCore.Authorization;

namespace Betalish.Web.Pages.Public.Signups;

[AllowAnonymous]
public class SignupSuccessModel(IUserToken userToken) : UserTokenPageModel(userToken)
{
    public void OnGet()
    {
    }
}
