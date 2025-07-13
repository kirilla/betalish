namespace Betalish.Web.Models;

public class EmailHeader
{
    public int Id { get; set; }

    public int AttackId { get; set; }

    public required string ToName { get; set; }
    public required string ToAddress { get; set; }

    public required string FromName { get; set; }
    public required string FromAddress { get; set; }

    public string? ReplyToName { get; set; }
    public string? ReplyToAddress { get; set; }

    public required string Subject { get; set; }

    public EmailStatus EmailStatus { get; set; }

    public DateTime? Created { get; set; }
    public DateTime? Sent { get; set; }
}
