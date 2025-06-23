using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.ClientEmailMessages.RemoveClientEmailMessages;

public class RemoveClientEmailMessagesCommandModel
{
    [Required]
    public EmailStatus? EmailStatus { get; set; }
}
