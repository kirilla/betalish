using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.UserPhones.AddAccountPhone;

public class AddAccountPhoneCommandModel
{
    [RegularExpression(Pattern.Common.Phone.Number)]
    [Required(ErrorMessage = "Skriv ditt telefonnummer.")]
    [StringLength(
        MaxLengths.Common.Phone.Number,
        ErrorMessage = "Skriv kortare.")]
    public string Number { get; set; } = string.Empty;
}
