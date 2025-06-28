using Betalish.Common.Validation;
using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.Signups.SignupForService;

public class SignupForServiceCommandModel
{
    // Person
    [RegularExpression(
        Pattern.Common.Ssn.Ssn12Permissive,
        ErrorMessage = "12 siffror")]
    [Required(
        ErrorMessage = "Skriv ditt personnummer.")]
    [StringLength(
        MaxLengths.Common.Ssn.Ssn12Permissive,
        ErrorMessage = "Skriv kortare.")]
    public string Ssn12 { get; set; }

    [RegularExpression(
        Pattern.Common.SomeContent,
        ErrorMessage = "Skriv ditt namn")]
    [Required(
        ErrorMessage = "Skriv ditt namn.")]
    [StringLength(
        MaxLengths.Common.Person.Name,
        ErrorMessage = "Skriv kortare.")]
    public string PersonName { get; set; }

    [RegularExpression(
        Pattern.Common.Email.Address,
        ErrorMessage = "Skriv en giltig epostadress")]
    [Required(
        ErrorMessage = "Skriv din epostadress.")]
    [StringLength(
        MaxLengths.Common.Email.Address,
        ErrorMessage = "Skriv kortare.")]
    public string EmailAddress { get; set; }

    [RegularExpression(
        Pattern.Common.Phone.Number,
        ErrorMessage = "Skriv ett giltigt telefonnummer.")]
    [Required(
        ErrorMessage = "Skriv ditt telefonnummer.")]
    [StringLength(
        MaxLengths.Common.Phone.Number,
        ErrorMessage = "Skriv kortare.")]
    public string PhoneNumber { get; set; }
}
