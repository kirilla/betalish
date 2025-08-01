namespace Betalish.Application.Reports.UnpaidReports;

public class UnpaidReportResultsModel
{
    public List<UnpaidInvoice> Invoices { get; set; } = [];

    public decimal Total { get; set; }
    public decimal LeftToPay { get; set; }
}
