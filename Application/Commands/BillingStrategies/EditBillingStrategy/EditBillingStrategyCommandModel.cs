using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.BillingStrategies.EditBillingStrategy;

public class EditBillingStrategyCommandModel
{
    public int Id { get; set; }

    [RegularExpression(Pattern.Common.SomeContent)]
    [Required(ErrorMessage = "Ge strategin ett namn.")]
    [StringLength(
        MaxLengths.Domain.BillingStrategy.Name,
        ErrorMessage = "Skriv kortare.")]
    public string? Name { get; set; }

    public bool Interest { get; set; }
    public bool Reminder { get; set; }
    public bool Demand { get; set; }
    public bool Collect { get; set; }
}
