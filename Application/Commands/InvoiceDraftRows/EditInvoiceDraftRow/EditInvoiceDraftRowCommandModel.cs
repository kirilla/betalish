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
}
