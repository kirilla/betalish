using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.Sessions.SignInBySsn;

public class SignInBySsnCommandModel
{
    [RegularExpression(
        Pattern.Common.Ssn.Ssn12Permissive,
        ErrorMessage = "12 siffror")]
    [StringLength(
        MaxLengths.Common.Ssn.Ssn12Permissive,
        ErrorMessage = "12 siffror")]
    public string Ssn12 { get; set; }

    [StringLength(MaxLengths.Common.Password.Clear)]
    public string Password { get; set; }
}
