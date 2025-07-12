using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.Account.RegisterAccount;

public class RegisterAccountCommandModel
{
    [RegularExpression(
        Pattern.Common.Ssn.Ssn12Permissive,
        ErrorMessage = "12 siffror")]
    [StringLength(
        MaxLengths.Common.Ssn.Ssn12Permissive,
        ErrorMessage = "12 siffror")]
    public string Ssn12 { get; set; } = string.Empty;

    [RegularExpression(Pattern.Common.SomeContent)]
    [StringLength(MaxLengths.Common.Person.FullName)]
    public string Name { get; set; } = string.Empty;

    [RegularExpression(Pattern.Common.Email.Address)]
    [StringLength(MaxLengths.Common.Email.Address)]
    public string EmailAddress { get; set; } = string.Empty;

    [StringLength(MaxLengths.Common.Password.Clear)]
    public string Password { get; set; } = string.Empty;
}
