namespace Betalish.Application.Reports.Payment.PaymentReport;

public class PaymentReportResultsModel
{
    public List<Domain.Entities.Payment> Payments { get; set; }
    public List<PaymentAccountingRow> PaymentAccountingRows { get; set; }
}
