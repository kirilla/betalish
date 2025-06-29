using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.EmailMessages.SendTestEmail;

public class SendTestEmailCommandModel
{
    [RegularExpression(Pattern.Common.SomeContent)]
    [StringLength(
        MaxLengths.Domain.EmailMessage.ToName,
        ErrorMessage = "Skriv kortare.")]
    public string ToName { get; set; }

    [RegularExpression(Pattern.Common.Email.Address)]
    [StringLength(
        MaxLengths.Domain.EmailMessage.ToAddress,
        ErrorMessage = "Skriv kortare.")]
    public string ToAddress { get; set; }

    [RegularExpression(Pattern.Common.SomeContent)]
    [StringLength(
        MaxLengths.Domain.EmailMessage.FromName,
        ErrorMessage = "Skriv kortare.")]
    public string FromName { get; set; }

    [RegularExpression(Pattern.Common.Email.Address)]
    [StringLength(
        MaxLengths.Domain.EmailMessage.FromAddress,
        ErrorMessage = "Skriv kortare.")]
    public string FromAddress { get; set; }

    [RegularExpression(Pattern.Common.SomeContent)]
    [StringLength(
        MaxLengths.Domain.EmailMessage.ReplyToName,
        ErrorMessage = "Skriv kortare.")]
    public string? ReplyToName { get; set; }

    [RegularExpression(Pattern.Common.Email.Address)]
    [StringLength(
        MaxLengths.Domain.EmailMessage.ReplyToAddress,
        ErrorMessage = "Skriv kortare.")]
    public string? ReplyToAddress { get; set; }

    [RegularExpression(Pattern.Common.SomeContent)]
    [StringLength(
        MaxLengths.Domain.EmailMessage.Subject,
        ErrorMessage = "Skriv kortare.")]
    public string Subject { get; set; }

    [RegularExpression(Pattern.Common.AnythingMultiLine)]
    [StringLength(
        MaxLengths.Domain.EmailMessage.HtmlBody,
        ErrorMessage = "Skriv kortare.")]
    public string HtmlBody { get; set; }

    [RegularExpression(Pattern.Common.AnythingMultiLine)]
    [StringLength(
        MaxLengths.Domain.EmailMessage.TextBody,
        ErrorMessage = "Skriv kortare.")]
    public string TextBody { get; set; }
}
