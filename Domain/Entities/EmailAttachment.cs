using System.ComponentModel.DataAnnotations;

namespace Betalish.Domain.Entities;

public class EmailAttachment
{
    public int Id { get; set; }

    public required byte[] Data { get; set; }

    public int ContentLength { get; set; }

    public required string Name { get; set; }
    public required string ContentType { get; set; }

    public int EmailMessageId { get; set; }
    public EmailMessage EmailMessage { get; set; } = null!;
}
