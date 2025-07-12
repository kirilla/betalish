namespace Betalish.Domain.Entities;

public class ClientEmailMessage : ICreatedDateTime
{
    public int Id { get; set; }

    public string ToName { get; set; }
    public string ToAddress { get; set; }

    public string FromName { get; set; }
    public string FromAddress { get; set; }

    public string? ReplyToName { get; set; }
    public string? ReplyToAddress { get; set; }

    public string Subject { get; set; }

    public string HtmlBody { get; set; }
    public string TextBody { get; set; }

    public EmailStatus EmailStatus { get; set; }

    public DateTime? Created { get; set; }
    public DateTime? Sent { get; set; }

    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;
}
