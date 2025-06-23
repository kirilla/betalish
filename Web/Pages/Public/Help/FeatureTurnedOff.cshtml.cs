using Microsoft.AspNetCore.Authorization;

namespace Betalish.Web.Pages.Public.Help;

[AllowAnonymous]
public class FeatureTurnedOffModel(
    IUserToken userToken) : UserTokenPageModel(userToken)
{
    public void OnGet()
    {
    }
}
