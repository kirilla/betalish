using Microsoft.AspNetCore.Authorization;

namespace Betalish.Web.Pages.Public.Account;

[AllowAnonymous]
public class RegisterAccountSuccessModel(
    IUserToken userToken) : UserTokenPageModel(userToken)
{
    public void OnGet()
    {
    }
}
