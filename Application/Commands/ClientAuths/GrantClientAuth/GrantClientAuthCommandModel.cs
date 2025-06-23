using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.ClientAuths.GrantClientAuth;

public class GrantClientAuthCommandModel
{
    public int UserId { get; set; }

    [Required]
    public int? ClientId { get; set; }
}
