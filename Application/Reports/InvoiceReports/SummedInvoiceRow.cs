namespace Betalish.Application.Reports.InvoiceReports;

public class SummedInvoiceRow
{
    public required int ArticleNumber { get; set; }
    public required string ArticleName { get; set; }
    public required decimal NetAmount { get; set; }
    public required decimal Quantity { get; set; }
}
