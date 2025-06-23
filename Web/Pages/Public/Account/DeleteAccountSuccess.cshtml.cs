using Microsoft.AspNetCore.Authorization;

namespace Betalish.Web.Pages.Public.Account;

[AllowAnonymous]
public class DeleteAccountSuccessModel(
    IUserToken userToken) : UserTokenPageModel(userToken)
{
    public void OnGet()
    {
    }
}
