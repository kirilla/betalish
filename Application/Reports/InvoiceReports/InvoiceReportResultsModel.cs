namespace Betalish.Application.Reports.InvoiceReports;

public class InvoiceReportResultsModel
{
    public List<SummedInvoiceRow> SummedInvoiceRows { get; set; } = [];
    public List<SummedInvoiceAccountingRow> SummedInvoiceAccountingRows { get; set; } = [];
}
