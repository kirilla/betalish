using System.Text.Json.Serialization;

namespace Betalish.Common.Settings;

public class SmtpConfiguration
{
    public bool Active { get; set; }

    public required string FromName { get; set; }
    public required string FromAddress { get; set; }

    [JsonIgnore]
    public string Password { get; set; } = null!;

    public required string Host { get; set; }

    public int Port { get; set; }
}
