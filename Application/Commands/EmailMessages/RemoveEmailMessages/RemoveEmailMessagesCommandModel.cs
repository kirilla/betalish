using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.EmailMessages.RemoveEmailMessages;

public class RemoveEmailMessagesCommandModel
{
    [Required]
    public EmailStatus? EmailStatus { get; set; }
}
