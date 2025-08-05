using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.PaymentTerms.AddPaymentTerms;

public class AddPaymentTermsCommandModel
{
    [RegularExpression(Pattern.Common.SomeContent)]
    [Required(ErrorMessage = "Ge betalvillkoren ett namn.")]
    [StringLength(
        MaxLengths.Domain.PaymentTerms.Name, 
        ErrorMessage = "Skriv kortare.")]
    public string? Name { get; set; }

    public bool Interest { get; set; }
    public bool Reminder { get; set; }
    public bool Demand { get; set; }
    public bool Collect { get; set; }

    [Range(Defaults.Invoice.PaymentTermDays.Min,
        Defaults.Invoice.PaymentTermDays.Max,
        ErrorMessage = "Ogiltigt antal dagar.")]
    [Required(ErrorMessage = "Ange antal dagar.")]
    public int? PaymentTermDays { get; set; }

    [RegularExpression(
        Pattern.Common.Decimal.Unsigned.TwoOptionalDecimals,
        ErrorMessage = "Belopp anges med två decimaler.")]
    public string? MinToConsiderPaid { get; set; }
}
