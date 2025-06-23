using System.ComponentModel.DataAnnotations;

namespace Betalish.Domain.Entities;

public class EmailAttachment
{
    public int Id { get; set; }

    public byte[] Data { get; set; }

    public int ContentLength { get; set; }

    [Required]
    [StringLength(MaxLengths.Domain.EmailAttachment.Name)]
    public string Name { get; set; }

    [Required]
    [StringLength(MaxLengths.Domain.EmailAttachment.ContentType)]
    public string ContentType { get; set; }

    public int EmailMessageId { get; set; }
    public EmailMessage EmailMessage { get; set; }
}
