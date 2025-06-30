using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.UserEmails.RemoveAccountEmail;

public class RemoveAccountEmailCommandModel
{
    [Required(ErrorMessage = "Välj epostadress.")]
    public int? Id { get; set; }

    public bool Confirmed { get; set; }
}
