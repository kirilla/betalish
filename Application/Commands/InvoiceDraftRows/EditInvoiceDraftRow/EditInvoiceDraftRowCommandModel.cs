using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.InvoiceDraftRows.EditInvoiceDraftRow;

public class EditInvoiceDraftRowCommandModel
{
    public int Id { get; set; }

    [RegularExpression(
        Pattern.Common.Decimal.Unsigned.TwoOptionalDecimals,
        ErrorMessage = "Ange kvantitet med siffror")]
    [Required(ErrorMessage = "Ange kvantitet.")]
    public string? Quantity { get; set; }

    // TODO: UnitPrice
    // for per-customer changes at draft-time
}
