using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.UserPhones.RemoveAccountPhone;

public class RemoveAccountPhoneCommandModel
{
    [Required(ErrorMessage = "Välj telefonnummer.")]
    public int? Id { get; set; }
}
