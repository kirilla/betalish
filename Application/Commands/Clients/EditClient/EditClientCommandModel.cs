using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.Clients.EditClient;

public class EditClientCommandModel
{
    public int Id { get; set; }

    [RegularExpression(Pattern.Common.SomeContent)]
    [Required(ErrorMessage = "Ange namn.")]
    [StringLength(
        MaxLengths.Domain.Client.Name,
        ErrorMessage = "Skriv kortare.")]
    public string Name { get; set; } = string.Empty;

    [RegularExpression(Pattern.Common.Email.Address)]
    [Required(ErrorMessage = "Ange epostadress.")]
    [StringLength(
        MaxLengths.Domain.Client.Address,
        ErrorMessage = "Skriv kortare.")]
    public string Address { get; set; } = string.Empty;
}
