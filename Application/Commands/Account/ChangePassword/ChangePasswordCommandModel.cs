using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.Account.ChangePassword;

public class ChangePasswordCommandModel
{
    [Required(ErrorMessage = "Skriv ditt nuvarande lösenord.")]
    [StringLength(MaxLengths.Common.Password.Clear)]
    public string OldPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Skriv ditt nya lösenord.")]
    [StringLength(MaxLengths.Common.Password.Clear)]
    public string NewPassword { get; set; } = string.Empty;
}
