namespace Betalish.Application.Reports.UnpaidReports;

public interface IUnpaidReport
{
    Task<UnpaidReportResultsModel> Execute(
        IUserToken userToken, UnpaidReportQueryModel model);

    bool IsPermitted(IUserToken userToken);
}
