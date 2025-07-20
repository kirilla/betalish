using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.InvoiceRanges.EditInvoiceRange;

public class EditInvoiceRangeCommandModel
{
    public int Id { get; set; }

    [Range(Ranges.Invoice.Number.Min, Ranges.Invoice.Number.Max)]
    [Required(ErrorMessage = "Ange första fakturanummer.")]
    public int StartNumber { get; set; }

    [Range(Ranges.Invoice.Number.Min, Ranges.Invoice.Number.Max)]
    [Required(ErrorMessage = "Ange sista fakturanummer.")]
    public int EndNumber { get; set; }

    [RegularExpression(Pattern.Common.SomeContent)]
    [StringLength(
        MaxLengths.Domain.InvoiceRange.Comment,
        ErrorMessage = "Skriv kortare.")]
    public string? Comment { get; set; }

    [RegularExpression(
        Pattern.Common.Date.Iso8601Permissive,
        ErrorMessage = "Ange datum i formatet ÅÅÅÅ-MM-DD.")]
    public string? StartDate { get; set; }

    [RegularExpression(
        Pattern.Common.Date.Iso8601Permissive,
        ErrorMessage = "Ange datum i formatet ÅÅÅÅ-MM-DD.")]
    public string? EndDate { get; set; }
}
