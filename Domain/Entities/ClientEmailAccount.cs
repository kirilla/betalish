namespace Betalish.Domain.Entities;

public class ClientEmailAccount
{
    public int Id { get; set; }

    public string FromName { get; set; }
    public string FromAddress { get; set; }

    public string? ReplyToName { get; set; }
    public string? ReplyToAddress { get; set; }

    public string Password { get; set; }
    
    public string SmtpHost { get; set; }
    public int SmtpPort { get; set; }

    public int ClientId { get; set; }
    public Client Client { get; set; }
}
