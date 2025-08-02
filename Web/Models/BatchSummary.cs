namespace Betalish.Web.Models;

public class BatchSummary
{
    public int Id { get; set; }

    public required string Name { get; set; }
    
    public int InvoiceCount { get; set; }
    public int InvoiceDraftCount { get; set; }
}
