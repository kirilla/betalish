namespace Betalish.Domain.Entities;

public class InvoicePlan
{
    public int Id { get; set; }

    // Payment
    public decimal? MinToConsiderPaid { get; set; }

    // Interest
    public bool Interest { get; set; }

    // Fees
    public decimal? ReminderFee { get; set; }
    public decimal? DemandFee { get; set; }

    // Customer reachability
    public bool SendByEmail { get; set; }
    public bool SendPostal { get; set; }

    // Relations
    public Invoice Invoice { get; set; } = null!;
}
