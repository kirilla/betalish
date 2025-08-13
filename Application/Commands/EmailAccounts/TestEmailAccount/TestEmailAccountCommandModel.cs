using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.EmailAccounts.TestEmailAccount;

public class TestEmailAccountCommandModel
{
    public int Id { get; set; }

    [RegularExpression(Pattern.Common.SomeContent)]
    [StringLength(
        MaxLengths.Domain.EmailMessage.ToName,
        ErrorMessage = "Skriv kortare.")]
    public string ToName { get; set; } = string.Empty;

    [RegularExpression(Pattern.Common.Email.Address)]
    [StringLength(
        MaxLengths.Domain.EmailMessage.ToAddress,
        ErrorMessage = "Skriv kortare.")]
    public string ToAddress { get; set; } = string.Empty;
}
