using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.ClientEmailAccounts.EditClientEmailAccount;

public class EditClientEmailAccountCommandModel
{
    public int ClientEmailAccountId { get; set; }

    [RegularExpression(Pattern.Common.SomeContent)]
    [Required(ErrorMessage = "Ange avsändare.")]
    [StringLength(
        MaxLengths.Domain.ClientEmailAccount.FromName,
        ErrorMessage = "Skriv kortare.")]
    public string FromName { get; set; }

    [RegularExpression(Pattern.Common.Email.Address)]
    [Required(ErrorMessage = "Ange epostadress.")]
    [StringLength(
        MaxLengths.Domain.ClientEmailAccount.FromAddress,
        ErrorMessage = "Skriv kortare.")]
    public string FromAddress { get; set; }

    [RegularExpression(Pattern.Common.SomeContent)]
    [StringLength(
        MaxLengths.Domain.ClientEmailAccount.ReplyToName,
        ErrorMessage = "Skriv kortare.")]
    public string? ReplyToName { get; set; }

    [RegularExpression(Pattern.Common.Email.Address)]
    [StringLength(
        MaxLengths.Domain.ClientEmailAccount.ReplyToAddress,
        ErrorMessage = "Skriv kortare.")]
    public string? ReplyToAddress { get; set; }

    [RegularExpression(Pattern.Common.SomeContent)]
    [Required(ErrorMessage = "Ange SMTP-server.")]
    [StringLength(
        MaxLengths.Domain.ClientEmailAccount.SmtpHost,
        ErrorMessage = "Skriv kortare.")]
    public string SmtpHost { get; set; }

    [Required(ErrorMessage = "Ange SMTP-port.")]
    [Range(Ranges.Smtp.Port.Min, Ranges.Smtp.Port.Max)]
    public int SmtpPort { get; set; }
}
