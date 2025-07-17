using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.InvoiceTemplateRows.AddInvoiceTemplateRow;

public class AddInvoiceTemplateRowCommandModel
{
    public int InvoiceTemplateId { get; set; }

    [Required(ErrorMessage = "Ange artikel.")]
    public int? ArticleId { get; set; }

    [RegularExpression(
        Pattern.Common.Decimal.Unsigned.TwoOptionalDecimals,
        ErrorMessage = "Ange kvantitet med siffror")]
    [Required(ErrorMessage = "Ange kvantitet.")]
    public string? Quantity { get; set; }
}
