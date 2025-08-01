namespace Betalish.Domain.Entities;

public class Batch
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public List<Invoice> Invoices { get; set; } = [];
    public List<InvoiceDraft> InvoiceDrafts { get; set; } = [];
}
