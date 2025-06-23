using Microsoft.AspNetCore.Authorization;

namespace Betalish.Web.Pages.Public;

[AllowAnonymous]
public class PingModel : PageModel
{
    public PingModel()
    {
    }

    public void OnGet()
    {
    }
}
