namespace Betalish.Domain.Entities;

public class ClientEmailAccount
{
    public int Id { get; set; }

    public required string FromName { get; set; }
    public required string FromAddress { get; set; }

    public string? ReplyToName { get; set; }
    public string? ReplyToAddress { get; set; }

    public required string Password { get; set; }

    public required string SmtpHost { get; set; }
    public int SmtpPort { get; set; }

    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;
}
