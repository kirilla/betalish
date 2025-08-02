using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.BillingPlans.AddBillingPlan;

public class AddBillingPlanCommandModel
{
    [RegularExpression(Pattern.Common.SomeContent)]
    [Required(ErrorMessage = "Ge tidsplanen ett namn.")]
    [StringLength(
        MaxLengths.Domain.BillingPlan.Name, 
        ErrorMessage = "Skriv kortare.")]
    public string? Name { get; set; }
}
