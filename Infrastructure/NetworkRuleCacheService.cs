using Betalish.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Betalish.Infrastructure;

public class NetworkRuleCacheService(
    IServiceScopeFactory scopeFactory,
    IMemoryCache cache) : INetworkRuleCacheService
{
    private const string cacheKey = "network_rules";

    public async Task<List<NetworkRule>> GetNetworkRules()
    {
        using var scope = scopeFactory.CreateScope();

        var database = scope.ServiceProvider.GetRequiredService<IDatabaseService>();

        var list = new List<NetworkRule>();

        if (cache.TryGetValue(cacheKey, out list))
            return list ?? new List<NetworkRule>();

        list = await database.NetworkRules
            .AsNoTracking()
            .OrderByDescending(x => x.PrefixLength) // Higher specificity
            .ThenBy(x => x.BaseAddress)
            .ToListAsync();

        var options = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1),
        };

        cache.Set(cacheKey, list, options);

        return list;
    }

    public void InvalidateCache()
    {
        cache.Remove(cacheKey);
    }
}
