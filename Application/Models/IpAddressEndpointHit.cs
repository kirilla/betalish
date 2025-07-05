namespace Betalish.Application.Models;

public class IpAddressEndpointHit
{
    public string? IpAddress { get; set; }
    public Endpoint Endpoint { get; set; }
    public DateTime DateTime { get; set; }
}
