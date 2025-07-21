using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.InvoiceDraftRows.EditInvoiceDraftRow;

public class EditInvoiceDraftRowCommandModel
{
    public int Id { get; set; }

    [RegularExpression(
        Pattern.Common.SomeContent,
        ErrorMessage = "Ange artikel.")]
    [Required(ErrorMessage = "Ange artikel.")]
    public string? ArticleName { get; set; }

    [RegularExpression(
        Pattern.Common.Decimal.Unsigned.TwoOptionalDecimals,
        ErrorMessage = "Ange kvantitet.")]
    [Required(ErrorMessage = "Ange kvantitet.")]
    public string? Quantity { get; set; }

    [RegularExpression(
        Pattern.Common.Decimal.Unsigned.TwoOptionalDecimals,
        ErrorMessage = "Ange pris.")]
    [Required(ErrorMessage = "Ange pris.")]
    public string? UnitPrice { get; set; }

    [RegularExpression(
        Pattern.Common.Bookkeeping.Account,
        ErrorMessage = "Intäktskonto anges med fyra siffror")]
    [Required(ErrorMessage = "Ange intäktskonto.")]
    [StringLength(
        MaxLengths.Common.Bookkeeping.Account,
        ErrorMessage = "Skriv kortare.")]
    public string? RevenueAccount { get; set; }

    [RegularExpression(
        Pattern.Common.Bookkeeping.Account,
        ErrorMessage = "Moms-konto anges med fyra siffror")]
    [Required(ErrorMessage = "Ange moms-konto.")]
    [StringLength(
        MaxLengths.Common.Bookkeeping.Account,
        ErrorMessage = "Skriv kortare.")]
    public string? VatAccount { get; set; }
}
