using Betalish.Application.Auth;
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
            await Log(context, rule.Block);
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

    private async Task Log(HttpContext context, bool blocked)
    {
        using var scope = scopeFactory.CreateScope();

        var database = scope.ServiceProvider.GetRequiredService<IDatabaseService>();

        var request = new NetworkRequest()
        {
            Url = context.Request.GetDisplayUrl(),
            Method = context.Request.Method,
            IpAddress = context.Connection.RemoteIpAddress?.ToString(),
            UserAgent = context.Request.Headers?.UserAgent,
            Blocked = blocked,
        };

        database.NetworkRequests.Add(request);

        await database.SaveAsync(new NoUserToken());
    }
}
