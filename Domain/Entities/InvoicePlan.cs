namespace Betalish.Domain.Entities;

public class InvoicePlan
{
    public int Id { get; set; }

    // Relations
    public Invoice Invoice { get; set; } = null!;
}
