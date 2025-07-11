using Betalish.Application.Models;
using Betalish.Application.Queues.EndpointRateLimiting;
using Betalish.Application.Queues.IpAddressRateLimiting;
using Betalish.Application.Queues.SignInRateLimiting;

namespace Betalish.Web.Pages.Admin.RateLimiting;

public class ShowRateLimitingModel(
    IUserToken userToken,
    IEndpointRateLimiter endpointRateLimiter,
    IIpAddressRateLimiter ipAddressRateLimiter,
    ISignInRateLimiter signInRateLimiter,
    IDatabaseService database) : AdminPageModel(userToken)
{
    public List<LogItem> RateLimitLogItems { get; set; }

    public List<IpAddressEndpointHit> IpAddressEndpointHits { get; set; }
    public List<SignInAttempt> SignInAttempts { get; set; }
    public List<EndpointHit> EndpointHits { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            AssertIsAdmin();

            RateLimitLogItems = await database.LogItems
                .Where(x =>
                    x.LogItemKind == LogItemKind.EndpointRateLimited ||
                    x.LogItemKind == LogItemKind.IpAddressRateLimited ||
                    x.LogItemKind == LogItemKind.SignInRateLimited)
                .OrderByDescending(x => x.Created)
                .ToListAsync();

            IpAddressEndpointHits = ipAddressRateLimiter
                .ToList()
                .OrderBy(x => x.DateTime)
                .ToList();

            SignInAttempts = signInRateLimiter
                .ToList()
                .OrderBy(x => x.DateTime)
                .ToList();

            EndpointHits = endpointRateLimiter
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
