using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.BillingPlans.EditBillingPlan;

public class EditBillingPlanCommandModel
{
    public int Id { get; set; }

    [RegularExpression(Pattern.Common.SomeContent)]
    [Required(ErrorMessage = "Ge tidsplanen ett namn.")]
    [StringLength(
        MaxLengths.Domain.BillingPlan.Name,
        ErrorMessage = "Skriv kortare.")]
    public string? Name { get; set; }
}
