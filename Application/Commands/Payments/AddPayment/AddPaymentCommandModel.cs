using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.Payments.AddPayment;

public class AddPaymentCommandModel
{
    [RegularExpression(
        Pattern.Common.Decimal.Signed.TwoOptionalDecimals,
        ErrorMessage = "Belopp anges med två decimaler.")]
    [Required(ErrorMessage = "Ange belopp.")]
    public string? Amount { get; set; }
}
