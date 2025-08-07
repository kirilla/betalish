namespace Betalish.Domain.Entities;

public class InvoicePlan
{
    public int Id { get; set; }

    // Stages
    public bool Reminder { get; set; }
    public bool Demand { get; set; }
    public bool Collect { get; set; }

    // Time frame
    public int PaymentTermDays { get; set; }

    // Payment
    public decimal? MinToConsiderPaid { get; set; }

    // Interest
    public bool Interest { get; set; }

    // Fees
    public decimal? ReminderFee { get; set; }
    public decimal? DemandFee { get; set; }

    // Dates
    public DateOnly? DistributionDate { get; set; }
    
    public DateOnly? ReminderDate { get; set; }
    public DateOnly? ReminderDueDate { get; set; }

    public DateOnly? DemandDate { get; set; }
    public DateOnly? DemandDueDate { get; set; }

    public DateOnly? CollectDate { get; set; }

    // Customer reachability
    public bool SendByEmail { get; set; }
    public bool SendPostal { get; set; }

    // Distribution
    public DateTime? DistributionEmailSent { get; set; }
    public DateTime? DistributionInvoicePrinted { get; set; }

    // Reminder
    public DateTime? ReminderEmailSent { get; set; }
    public DateTime? ReminderInvoicePrinted { get; set; }

    // Demand
    public DateTime? DemandEmailSent { get; set; }
    public DateTime? DemandInvoicePrinted { get; set; }

    // Collect
    public DateTime? CollectEmailSent { get; set; }
    public DateTime? CollectInvoicePrinted { get; set; }

    // Relations
    public Invoice Invoice { get; set; } = null!;
}
