using Betalish.Application.Auth;
using Betalish.Application.Queues.NetworkRequests;
using Microsoft.AspNetCore.Http.Extensions;

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

        var rule = 
            (await cacheService.GetNetworkRules())
            .OrderByDescending(x => x.PrefixLength)
            .ThenBy(x => x.BaseAddress)
            .Where(x => x.NetworkContains(requestIp))
            .FirstOrDefault();

        if (rule == null)
        {
            await next(context);
            return;
        }

        if (rule.Log)
        {
            Log(context, rule.Block);
        }

        if (rule.Block)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;

            //context.Response.StatusCode = StatusCodes.Status403Forbidden;
            //await context.Response.WriteAsync("Access Denied");

            return;
        }

        await next(context);
        return;
    }

    private void Log(HttpContext context, bool blocked)
    {
        using var scope = scopeFactory.CreateScope();

        var networkRequestList = scope.ServiceProvider.GetRequiredService<INetworkRequestList>();

        var request = new NetworkRequest()
        {
            Url = context.Request.GetDisplayUrl(),
            Method = context.Request.Method,
            IpAddress = context.Connection.RemoteIpAddress?.ToString(),
            UserAgent = context.Request.Headers?.UserAgent,
            Blocked = blocked,
        };

        networkRequestList.Add(request);
    }
}
