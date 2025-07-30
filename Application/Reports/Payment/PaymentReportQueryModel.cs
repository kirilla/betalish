using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Reports.Payment;

public class PaymentReportQueryModel
{
    [RegularExpression(
        Pattern.Common.Date.Iso8601Permissive,
        ErrorMessage = "Ange datum i formatet ÅÅÅÅ-MM-DD.")]
    [Required(ErrorMessage = "Ange startdatum.")]
    public string? StartDate { get; set; }

    [RegularExpression(
        Pattern.Common.Date.Iso8601Permissive,
        ErrorMessage = "Ange datum i formatet ÅÅÅÅ-MM-DD.")]
    [Required(ErrorMessage = "Ange slutdatum.")]
    public string? EndDate { get; set; }
}
