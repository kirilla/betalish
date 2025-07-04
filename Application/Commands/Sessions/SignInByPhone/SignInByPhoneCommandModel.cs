using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.Sessions.SignInByPhone;

public class SignInByPhoneCommandModel
{
    [RegularExpression(
        Pattern.Common.Phone.Number,
        ErrorMessage = "Skriv ditt telefonnummer.")]
    [StringLength(MaxLengths.Common.Phone.Number)]
    public string PhoneNumber { get; set; }

    [StringLength(MaxLengths.Common.Password.Clear)]
    public string Password { get; set; }
}
