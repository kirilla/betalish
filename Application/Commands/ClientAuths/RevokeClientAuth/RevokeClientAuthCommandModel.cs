using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.ClientAuths.RevokeClientAuth;

public class RevokeClientAuthCommandModel
{
    public int UserId { get; set; }

    [Required]
    public int? ClientId { get; set; }
}
