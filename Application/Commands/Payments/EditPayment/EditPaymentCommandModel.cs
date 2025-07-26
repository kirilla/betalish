using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.Payments.EditPayment;

public class EditPaymentCommandModel
{
    public int Id { get; set; }

    [RegularExpression(
        Pattern.Common.Decimal.Signed.TwoOptionalDecimals,
        ErrorMessage = "Belopp anges med två decimaler.")]
    [Required(ErrorMessage = "Ange belopp.")]
    public string? Amount { get; set; }
}
