using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.UserEmails.RemoveUserEmail;

public class RemoveUserEmailCommandModel
{
    [Required(ErrorMessage = "Välj den epostadress som du vill ta bort.")]
    public int? UserEmailId { get; set; }

    public bool Confirmed { get; set; }
}
