using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Reports.UnpaidReports;

public class UnpaidReportQueryModel
{
    [RegularExpression(
        Pattern.Common.Date.Iso8601Permissive,
        ErrorMessage = "Ange datum i formatet ÅÅÅÅ-MM-DD.")]
    [Required(ErrorMessage = "Ange datum.")]
    public string? ReportDate { get; set; }
}
