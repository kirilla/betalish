using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.ClientEmailAccounts.SetClientEmailAccountPassword;

public class SetClientEmailAccountPasswordCommandModel
{
    public int Id { get; set; }

    [RegularExpression(Pattern.Common.Anything)]
    [Required(ErrorMessage = "Ange lösenord.")]
    [StringLength(
        MaxLengths.Domain.ClientEmailAccount.Password,
        ErrorMessage = "Skriv kortare.")]
    public string Password { get; set; }
}
