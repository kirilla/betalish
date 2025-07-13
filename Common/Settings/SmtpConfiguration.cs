using System.Text.Json.Serialization;

namespace Betalish.Common.Settings;

public class SmtpConfiguration
{
    public bool Active { get; set; }

    public required string FromName { get; set; }
    public required string FromAddress { get; set; }

    [JsonIgnore]
    public required string Password { get; set; }

    public required string SmtpHost { get; set; }

    public int SmtpPort { get; set; }
}
