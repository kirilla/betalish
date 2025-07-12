using System.Net;

namespace Betalish.Domain.Entities;

public class NetworkRule : 
    ICreatedDateTime, 
    IUpdatedDateTime,
    IFormatOnSave
{
    public int Id { get; set; }

    public string BaseAddress { get; set; }
    public int PrefixLength { get; set; }

    public bool Active { get; set; }

    public bool Block { get; set; }
    public bool Log { get; set; }

    public string? Description { get; set; }

    public DateTime? Created { get; set; }
    public DateTime? Updated { get; set; }

    private readonly Lazy<IPNetwork> _ipNetwork;

    public IPNetwork IPNetwork => _ipNetwork.Value;

    public string NetworkCidr => $"{BaseAddress}/{PrefixLength}";

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

        return new IPNetwork(baseIp, PrefixLength);
    }

    public void FormatOnSave()
    {
        Description = Description?
            .Truncate(MaxLengths.Domain.NetworkRule.Description);
    }
}
