using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.BillingPlanItems.AddBillingPlanItem;

public class AddBillingPlanItemCommandModel
{
    public int BillingPlanId { get; set; }

    [Required(ErrorMessage = "Ange typ av händelse.")]
    public PlannedItemKind? PlannedItemKind { get; set; }

    public int OnDay { get; set; }
}
