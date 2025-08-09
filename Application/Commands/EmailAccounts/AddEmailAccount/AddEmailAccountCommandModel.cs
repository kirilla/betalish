using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.EmailAccounts.AddEmailAccount;

public class AddEmailAccountCommandModel
{
    [RegularExpression(Pattern.Common.SomeContent)]
    [Required(ErrorMessage = "Ange avsändare.")]
    [StringLength(
        MaxLengths.Domain.EmailAccount.FromName,
        ErrorMessage = "Skriv kortare.")]
    public string FromName { get; set; } = string.Empty;

    [RegularExpression(Pattern.Common.Email.Address)]
    [Required(ErrorMessage = "Ange epostadress.")]
    [StringLength(
        MaxLengths.Domain.EmailAccount.FromAddress,
        ErrorMessage = "Skriv kortare.")]
    public string FromAddress { get; set; } = string.Empty;

    [RegularExpression(Pattern.Common.SomeContent)]
    [StringLength(
        MaxLengths.Domain.EmailAccount.ReplyToName,
        ErrorMessage = "Skriv kortare.")]
    public string? ReplyToName { get; set; }

    [RegularExpression(Pattern.Common.Email.Address)]
    [StringLength(
        MaxLengths.Domain.EmailAccount.ReplyToAddress,
        ErrorMessage = "Skriv kortare.")]
    public string? ReplyToAddress { get; set; }

    [RegularExpression(Pattern.Common.Anything)]
    [Required(ErrorMessage = "Ange lösenord.")]
    [StringLength(
        MaxLengths.Domain.EmailAccount.Password,
        ErrorMessage = "Skriv kortare.")]
    public string Password { get; set; } = string.Empty;

    [RegularExpression(Pattern.Common.SomeContent)]
    [Required(ErrorMessage = "Ange SMTP-server.")]
    [StringLength(
        MaxLengths.Domain.EmailAccount.SmtpHost,
        ErrorMessage = "Skriv kortare.")]
    public string SmtpHost { get; set; } = string.Empty;

    [Required(ErrorMessage = "Ange SMTP-port.")]
    [Range(Ranges.Smtp.Port.Min, Ranges.Smtp.Port.Max)]
    public int SmtpPort { get; set; }
}
