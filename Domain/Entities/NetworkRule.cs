using System.Net;

namespace Betalish.Domain.Entities;

public class NetworkRule : ICreatedDateTime, IUpdatedDateTime
{
    public int Id { get; set; }

    public string Range { get; set; }

    public string? BaseAddress2 { get; set; }
    public int? Prefix2 { get; set; }

    public bool Blocked { get; set; }

    public DateTime? Created { get; set; }
    public DateTime? Updated { get; set; }

    private Lazy<IPNetwork> _ipNetwork;
    private Lazy<string> _baseAddress;
    private Lazy<int> _prefix;

    public IPNetwork IPNetwork => _ipNetwork.Value;
    public string BaseAddress => _baseAddress.Value;
    public int Prefix => _prefix.Value;

    public NetworkRule()
    {
        _ipNetwork = new Lazy<IPNetwork>(() => CreateIPNetwork());
        _baseAddress = new Lazy<string>(() => CreateBaseAddress());
        _prefix = new Lazy<int>(() => CreatePrefix());
    }

    public bool IsInRange(IPAddress ipAddress)
    {
        return IPNetwork.Contains(ipAddress);
    }

    private IPNetwork CreateIPNetwork()
    {
        var parts = Range.Split('/');

        IPAddress baseIp = IPAddress.Parse(parts[0]);

        int prefixLength = int.Parse(parts[1]);

        return new IPNetwork(baseIp, prefixLength);
    }

    private string CreateBaseAddress()
    {
        var parts = Range.Split('/');

        return parts[0];
    }

    private int CreatePrefix()
    {
        var parts = Range.Split('/');

        return int.Parse(parts[1]);
    }
}
