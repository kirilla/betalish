namespace Betalish.Application.Reports.Payment.PaymentReport;

public interface IPaymentReport
{
    Task<PaymentReportResultsModel> Execute(
        IUserToken userToken, PaymentReportQueryModel model);

    bool IsPermitted(IUserToken userToken);
}
