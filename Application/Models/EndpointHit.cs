namespace Betalish.Application.Models;

public class EndpointHit
{
    public DateTime DateTime { get; set; }
    public Endpoint Endpoint { get; set; }
    public string? IpAddress { get; set; }
    public int? UserId { get; set; }
}
