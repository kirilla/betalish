namespace Betalish.Domain.Entities;

public class Batch
{
    public int Id { get; set; }

    public required string Name { get; set; }

    // Relations
    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;

    public List<Invoice> Invoices { get; set; } = [];
    public List<InvoiceDraft> InvoiceDrafts { get; set; } = [];
}
