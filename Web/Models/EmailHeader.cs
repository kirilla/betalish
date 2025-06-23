namespace Betalish.Web.Models;

public class EmailHeader
{
    public int Id { get; set; }

    public int AttackId { get; set; }

    public string ToName { get; set; }
    public string ToAddress { get; set; }

    public string FromName { get; set; }
    public string FromAddress { get; set; }

    public string? ReplyToName { get; set; }
    public string? ReplyToAddress { get; set; }

    public string Subject { get; set; }

    public EmailStatus EmailStatus { get; set; }

    public DateTime? Created { get; set; }
    public DateTime? Sent { get; set; }
}
