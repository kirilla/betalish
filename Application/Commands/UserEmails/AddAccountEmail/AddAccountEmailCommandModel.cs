using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.UserEmails.AddAccountEmail;

public class AddAccountEmailCommandModel
{
    [RegularExpression(Pattern.Common.Email.Address)]
    [Required(ErrorMessage = "Skriv din epostadress.")]
    [StringLength(
        MaxLengths.Common.Email.Address,
        ErrorMessage = "Skriv kortare.")]
    public string Address { get; set; }
}
