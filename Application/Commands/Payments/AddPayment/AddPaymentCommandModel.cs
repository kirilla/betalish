using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.Payments.AddPayment;

public class AddPaymentCommandModel
{
    [RegularExpression(
        Pattern.Common.Decimal.Signed.TwoOptionalDecimals,
        ErrorMessage = "Belopp anges med två decimaler.")]
    [Required(ErrorMessage = "Ange belopp.")]
    public string? Amount { get; set; }

    [RegularExpression(
        Pattern.Common.Date.Iso8601Permissive,
        ErrorMessage = "Ange datum i formatet ÅÅÅÅ-MM-DD.")]
    [Required(ErrorMessage = "Ange datum.")]
    public string? Date { get; set; }

    [Required(ErrorMessage = "Ange betalsätt.")]
    public PaymentMethod? PaymentMethod { get; set; }

    [Required(ErrorMessage = "Ange konto.")]
    public int? PaymentAccountId { get; set; }
}
