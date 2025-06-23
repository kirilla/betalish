namespace Betalish.Common.Settings;

public class FirewallConfiguration
{
    public bool IsFiltering { get; set; }
    public bool BlockByDefault { get; set; }
    public bool LogBlockedRequest { get; set; }
}
