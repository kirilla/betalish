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
        if (!_config.IsFiltering)
        {
            await next(context);
            return;
        }

        var requestIp = context.Connection.RemoteIpAddress;

        if (requestIp == null)
        {
            await next(context);
            return;
        }

        var decision = await Decide(requestIp);

        if (decision == FirewallDecision.AllowedByDefault ||
            decision == FirewallDecision.AllowedByRule)
        {
            await next(context);
            return;
        }

        context.Response.StatusCode = StatusCodes.Status404NotFound;

        //context.Response.StatusCode = StatusCodes.Status403Forbidden;
        //await context.Response.WriteAsync("Access Denied");

        if (_config.LogBlockedRequest &&
            decision == FirewallDecision.BlockedByDefault)
        {
            // NOTE: Only log the BlockedByDefault case.
            //
            // No logging for explicitly blocked IP ranges.

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
    }

    private async Task<FirewallDecision> Decide(IPAddress requestIp)
    {
        var rules = await cacheService.GetNetworkRules();

        foreach (var rule in rules)
        {
            if (rule.NetworkContains(requestIp))
            {
                if (rule.Blocked)
                    return FirewallDecision.BlockedByRule;
                else
                    return FirewallDecision.AllowedByRule;
            }
        }

        if (_config.BlockByDefault)
            return FirewallDecision.BlockedByDefault;
        else
            return FirewallDecision.AllowedByDefault;
    }
}
