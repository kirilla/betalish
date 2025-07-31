namespace Betalish.Application.Reports.InvoiceReports;

public interface IInvoiceReport
{
    Task<InvoiceReportResultsModel> Execute(
        IUserToken userToken, InvoiceReportQueryModel model);

    bool IsPermitted(IUserToken userToken);
}
