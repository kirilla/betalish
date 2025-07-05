using Betalish.Application.Models;
using Betalish.Application.Queues.IpAddressRateLimiting;

namespace Betalish.Web.Pages.Admin.RateLimiting;

public class ShowRateLimitingModel(
    IUserToken userToken,
    IIpAddressRateLimiter rateLimiter,
    IDatabaseService database) : AdminPageModel(userToken)
{
    public List<IpAddressEndpointHit> EndpointHits { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            await AssertAdminAuthorization(database);

            EndpointHits = rateLimiter
                .ToList()
                .OrderBy(x => x.DateTime)
                .ToList();

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
