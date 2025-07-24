namespace Betalish.Domain.Entities;

public class DraftBalanceRow
{
    public int Id { get; set; }

    public required int InvoiceNumber { get; set; }

    public required decimal Amount { get; set; }

    public int InvoiceId { get; set; }
    public Invoice Invoice { get; set; } = null!;

    public int InvoiceDraftId { get; set; }
    public InvoiceDraft InvoiceDraft { get; set; } = null!;
}
