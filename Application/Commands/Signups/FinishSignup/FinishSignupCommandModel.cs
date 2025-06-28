using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.Signups.FinishSignup;

public class FinishSignupCommandModel
{
    public Guid? Guid { get; set; }

    [StringLength(MaxLengths.Common.Password.Clear)]
    public string Password { get; set; }
}
