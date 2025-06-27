using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.UserEmails.RemoveUserEmail;

public class RemoveUserEmailCommandModel
{
    [Required(ErrorMessage = "Välj epostadress.")]
    public int? Id { get; set; }

    public bool Confirmed { get; set; }
}
