using System.Text.Json.Serialization;

namespace Betalish.Common.Settings;

public class SmtpConfiguration
{
    public bool Active { get; set; }

    public required string FromName { get; set; }
    public required string FromAddress { get; set; }

    [JsonIgnore]
    public string Password { get; set; } = null!;

    public required string SmtpHost { get; set; }

    public int SmtpPort { get; set; }
}
