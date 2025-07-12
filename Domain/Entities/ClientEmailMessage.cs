namespace Betalish.Domain.Entities;

public class ClientEmailMessage : ICreatedDateTime
{
    public int Id { get; set; }

    public required string ToName { get; set; }
    public required string ToAddress { get; set; }

    public required string FromName { get; set; }
    public required string FromAddress { get; set; }

    public string? ReplyToName { get; set; }
    public string? ReplyToAddress { get; set; }

    public required string Subject { get; set; }

    public required string HtmlBody { get; set; }
    public required string TextBody { get; set; }

    public EmailStatus EmailStatus { get; set; }

    public DateTime? Created { get; set; }
    public DateTime? Sent { get; set; }

    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;
}
