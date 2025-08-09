using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.EmailAccounts.SetEmailAccountPassword;

public class SetEmailAccountPasswordCommandModel
{
    public int Id { get; set; }

    [RegularExpression(Pattern.Common.Anything)]
    [Required(ErrorMessage = "Ange lösenord.")]
    [StringLength(
        MaxLengths.Domain.EmailAccount.Password,
        ErrorMessage = "Skriv kortare.")]
    public string Password { get; set; } = string.Empty;
}
