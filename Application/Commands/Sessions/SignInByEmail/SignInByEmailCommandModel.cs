using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.Sessions.SignInByEmail;

public class SignInByEmailCommandModel
{
    [RegularExpression(
        Pattern.Common.Email.Address,
        ErrorMessage = "Skriv din epostadress.")]
    [StringLength(MaxLengths.Common.Email.Address)]
    public string EmailAddress { get; set; }

    [StringLength(MaxLengths.Common.Password.Clear)]
    public string Password { get; set; }
}
