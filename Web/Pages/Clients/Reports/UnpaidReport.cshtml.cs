using Betalish.Application.Reports.UnpaidReports;

namespace Betalish.Web.Pages.Clients.Reports;

public class UnpaidReportModel(
    IUserToken userToken,
    IDateService dateService,
    IUnpaidReport report) : ClientPageModel(userToken)
{
    public UnpaidReportResultsModel ResultsModel { get; set; } = null!;

    [BindProperty]
    public UnpaidReportQueryModel QueryModel { get; set; } = new();

    public IActionResult OnGet()
    {
        try
        {
            if (!report.IsPermitted(UserToken))
                throw new NotPermittedException();

            var today = dateService.GetDateOnlyToday();

            QueryModel = new UnpaidReportQueryModel()
            {
                ReportDate = today.ToIso8601(),
            };

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            if (!report.IsPermitted(UserToken))
                throw new NotPermittedException();

            if (!ModelState.IsValid)
                return Page();

            ResultsModel = await report.Execute(UserToken, QueryModel);

            return Page();
        }
        catch (InvalidDateException)
        {
            ModelState.AddModelError(
                nameof(QueryModel.ReportDate),
                "Ogiltigt datum.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
