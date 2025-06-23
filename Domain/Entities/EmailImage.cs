using System.ComponentModel.DataAnnotations;

namespace Betalish.Domain.Entities;

public class EmailImage
{
    public int Id { get; set; }

    public byte[] Data { get; set; }

    public int ContentLength { get; set; }

    [Required]
    [StringLength(MaxLengths.Domain.EmailImage.Name)]
    public string Name { get; set; }

    [Required]
    [StringLength(MaxLengths.Domain.EmailImage.ContentType)]
    public string ContentType { get; set; }

    public int EmailMessageId { get; set; }
    public EmailMessage EmailMessage { get; set; }
}
