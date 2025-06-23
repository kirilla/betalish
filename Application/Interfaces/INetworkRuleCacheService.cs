namespace Betalish.Application.Interfaces;

public interface INetworkRuleCacheService
{
    Task<List<NetworkRule>> GetNetworkRules();
    
    void InvalidateCache();
}
