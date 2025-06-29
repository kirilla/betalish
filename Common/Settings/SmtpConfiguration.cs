namespace Betalish.Common.Settings;

public class SmtpConfiguration
{
    public bool Active { get; set; }

    public string FromName { get; set; }
    public string FromAddress { get; set; }
    public string Password { get; set; }
    public string SmtpHost { get; set; }

    public int SmtpPort { get; set; }
}
