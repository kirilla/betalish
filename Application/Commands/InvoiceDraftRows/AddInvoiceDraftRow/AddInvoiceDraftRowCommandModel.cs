using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.InvoiceDraftRows.AddInvoiceDraftRow;

public class AddInvoiceDraftRowCommandModel
{
    public int InvoiceDraftId { get; set; }

    [Required(ErrorMessage = "Ange artikel.")]
    public int? ArticleId { get; set; }

    [RegularExpression(
        Pattern.Common.Decimal.Unsigned.TwoOptionalDecimals,
        ErrorMessage = "Ange kvantitet med siffror")]
    [Required(ErrorMessage = "Ange kvantitet.")]
    public string? Quantity { get; set; }
}
