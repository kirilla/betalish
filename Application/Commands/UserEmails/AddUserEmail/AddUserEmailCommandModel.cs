using Betalish.Common.Validation;
using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.UserEmails.AddUserEmail;

public class AddUserEmailCommandModel
{
    [RegularExpression(Pattern.Common.Email.Address)]
    [Required(ErrorMessage = "Skriv din epostadress.")]
    [StringLength(
        MaxLengths.Common.Email.Address,
        ErrorMessage = "Skriv kortare.")]
    public string Address { get; set; }
}
