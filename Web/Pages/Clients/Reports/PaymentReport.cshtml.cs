using Betalish.Application.Reports.Payment;

namespace Betalish.Web.Pages.Clients.Reports;

public class PaymentReportModel(
    IUserToken userToken,
    IDateService dateService,
    IPaymentReport report) : ClientPageModel(userToken)
{
    public PaymentReportResultsModel ResultsModel { get; set; } = new();

    [BindProperty]
    public PaymentReportQueryModel QueryModel { get; set; } = new();

    public IActionResult OnGet()
    {
        try
        {
            if (!report.IsPermitted(UserToken))
                throw new NotPermittedException();

            var today = dateService.GetDateOnlyToday();

            QueryModel = new PaymentReportQueryModel()
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
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
