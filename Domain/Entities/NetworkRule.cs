using System.Net;

namespace Betalish.Domain.Entities;

public class NetworkRule : ICreatedDateTime, IUpdatedDateTime
{
    public int Id { get; set; }

    public string Range { get; set; }

    public string? BaseAddress2 { get; set; }
    public int? Prefix2 { get; set; } // TODO: PrefixLength

    public bool Blocked { get; set; }

    public DateTime? Created { get; set; }
    public DateTime? Updated { get; set; }

    private Lazy<IPNetwork> _ipNetwork;

    public IPNetwork IPNetwork => _ipNetwork.Value;

    public NetworkRule()
    {
        _ipNetwork = new Lazy<IPNetwork>(() => CreateIPNetwork());
    }

    public bool IsInRange(IPAddress ipAddress)
    {
        return IPNetwork.Contains(ipAddress);
    }

    private IPNetwork CreateIPNetwork()
    {
        IPAddress baseIp = IPAddress.Parse(BaseAddress2);

        return new IPNetwork(baseIp, Prefix2 ?? 0);
    }
}
