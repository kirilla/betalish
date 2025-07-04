using System.Net;

namespace Betalish.Application.Models;

public class EndpointHit
{
    public DateTime DateTime { get; set; }
    public Endpoint Endpoint { get; set; }
    public IPAddress? IpAddress { get; set; }
    public int? UserId { get; set; }
}
