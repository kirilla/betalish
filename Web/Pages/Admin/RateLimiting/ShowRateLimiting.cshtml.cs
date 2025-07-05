using Betalish.Application.Models;
using Betalish.Application.Queues.IpAddressRateLimiting;
using Betalish.Application.Queues.SignInRateLimiting;

namespace Betalish.Web.Pages.Admin.RateLimiting;

public class ShowRateLimitingModel(
    IUserToken userToken,
    IIpAddressRateLimiter ipAddressRateLimiter,
    ISignInRateLimiter signInRateLimiter,
    IDatabaseService database) : AdminPageModel(userToken)
{
    public List<IpAddressEndpointHit> IpAddressEndpointHits { get; set; }
    public List<SignInAttempt> SignInAttempts { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            await AssertAdminAuthorization(database);

            IpAddressEndpointHits = ipAddressRateLimiter
                .ToList()
                .OrderBy(x => x.DateTime)
                .ToList();

            SignInAttempts = signInRateLimiter
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
