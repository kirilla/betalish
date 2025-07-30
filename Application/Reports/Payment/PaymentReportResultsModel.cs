namespace Betalish.Application.Reports.Payment;

public class PaymentReportResultsModel
{
    //public List<Invoice> Invoices { get; set; } = [];
    public List<Domain.Entities.Payment> Payments { get; set; } = [];
    public List<PaymentAccountingRow> PaymentAccountingRows { get; set; } = [];

    public List<SummedPaymentAccountingRow> SummedPaymentAccountingRows { get; set; } = [];
    public List<PaymentDateSum> PaymentDateSums { get; set; } = [];
}
