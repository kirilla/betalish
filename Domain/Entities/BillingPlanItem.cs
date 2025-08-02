namespace Betalish.Domain.Entities;

public class BillingPlanItem
{
    public int Id { get; set; }

    public required PlannedItemKind PlannedItemKind { get; set; }

    public required int OnDay { get; set; }

    // Relations
    public int BillingPlanId { get; set; }
    public BillingPlan BillingPlan { get; set; } = null!;
}
