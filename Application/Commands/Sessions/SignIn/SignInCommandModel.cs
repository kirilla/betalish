using System.ComponentModel.DataAnnotations;
using Betalish.Common.Validation;

namespace Betalish.Application.Commands.Sessions.SignIn;

public class SignInCommandModel
{
    [RegularExpression(
        Pattern.Common.Email.Address,
        ErrorMessage = "Skriv din epostadress.")]
    [StringLength(MaxLengths.Common.Email.Address)]
    public string EmailAddress { get; set; }

    [StringLength(MaxLengths.Common.Password.Clear)]
    public string Password { get; set; }
}
