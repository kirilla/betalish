using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.BillingStrategies.AddBillingStrategy;

public class AddBillingStrategyCommandModel
{
    [RegularExpression(Pattern.Common.SomeContent)]
    [Required(ErrorMessage = "Ge strategin ett namn.")]
    [StringLength(
        MaxLengths.Domain.BillingStrategy.Name, 
        ErrorMessage = "Skriv kortare.")]
    public string? Name { get; set; }
}
