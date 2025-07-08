using System.Net;

namespace Betalish.Domain.Entities;

public class NetworkRule : ICreatedDateTime, IUpdatedDateTime
{
    public int Id { get; set; }

    public string? BaseAddress { get; set; }
    public int? PrefixLength { get; set; }

    public bool Blocked { get; set; }

    public DateTime? Created { get; set; }
    public DateTime? Updated { get; set; }

    private Lazy<IPNetwork> _ipNetwork;

    public IPNetwork IPNetwork => _ipNetwork.Value;

    public NetworkRule()
    {
        _ipNetwork = new Lazy<IPNetwork>(() => CreateIPNetwork());
    }

    public bool NetworkContains(IPAddress ipAddress)
    {
        return IPNetwork.Contains(ipAddress);
    }

    private IPNetwork CreateIPNetwork()
    {
        IPAddress baseIp = IPAddress.Parse(BaseAddress);

        return new IPNetwork(baseIp, PrefixLength ?? 0);
    }
}
