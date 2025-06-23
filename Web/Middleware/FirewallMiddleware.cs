using Betalish.Application.Auth;
using Microsoft.AspNetCore.Http.Extensions;
using System.Net;

namespace Betalish.Web.Middleware;

public class FirewallMiddleware(
    RequestDelegate next,
    IServiceScopeFactory scopeFactory,
    INetworkRuleCacheService cacheService,
    IOptions<FirewallConfiguration> firewallOptions)
{
    private readonly FirewallConfiguration _config = firewallOptions.Value;

    public async Task InvokeAsync(HttpContext context)
    {
        var requestIp = context.Connection.RemoteIpAddress;

        if (_config.IsFiltering &&
            requestIp != null &&
            await IsIpBlocked(requestIp))
        {
            //context.Response.StatusCode = StatusCodes.Status403Forbidden;
            //await context.Response.WriteAsync("Access Denied");

            context.Response.StatusCode = StatusCodes.Status404NotFound;

            if (_config.LogBlockedRequest)
            {
                using var scope = scopeFactory.CreateScope();

                var database = scope.ServiceProvider.GetRequiredService<IDatabaseService>();

                var request = new BlockedRequest()
                {
                    Url = context.Request.GetDisplayUrl(),
                    Method = context.Request.Method,
                    IpAddress = context.Connection.RemoteIpAddress?.ToString(),
                    UserAgent = context.Request.Headers?.UserAgent,
                };

                database.BlockedRequests.Add(request);

                await database.SaveAsync(new NoUserToken());
            }

            return;
        }

        await next(context);
    }

    private async Task<bool> IsIpBlocked(IPAddress requestIp)
    {
        var rules = await cacheService.GetNetworkRules();

        foreach (var rule in rules)
        {
            if (rule.IsInRange(requestIp))
            {
                return rule.Blocked;
            }
        }

        return _config.BlockByDefault;

        // Or simply: return false/true;
    }
}
