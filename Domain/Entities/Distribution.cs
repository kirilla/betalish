namespace Betalish.Domain.Entities;

public class Distribution
{
    public int Id { get; set; }

    public DateOnly? Date { get; set; }

    public bool Email { get; set; }
    public bool Postal { get; set; }

    public bool EmailSent { get; set; }
    public bool InvoicePrinted { get; set; }

    // Relations
    public Invoice Invoice { get; set; } = null!;
}
