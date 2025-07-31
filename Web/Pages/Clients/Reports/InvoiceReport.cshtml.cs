using Betalish.Application.Reports.InvoiceReports;

namespace Betalish.Web.Pages.Clients.Reports;

public class InvoiceReportModel(
    IUserToken userToken,
    IDateService dateService,
    IInvoiceReport report) : ClientPageModel(userToken)
{
    public InvoiceReportResultsModel ResultsModel { get; set; } = null!;

    [BindProperty]
    public InvoiceReportQueryModel QueryModel { get; set; } = new();

    public IActionResult OnGet()
    {
        try
        {
            if (!report.IsPermitted(UserToken))
                throw new NotPermittedException();

            var today = dateService.GetDateOnlyToday();

            QueryModel = new InvoiceReportQueryModel()
            {
                StartDate = today.ToIso8601(),
                EndDate = today.ToIso8601(),
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
                nameof(QueryModel.StartDate),
                "Ogiltiga datum.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
