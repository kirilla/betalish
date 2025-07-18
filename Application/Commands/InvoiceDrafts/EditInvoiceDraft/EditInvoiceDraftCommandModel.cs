using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.InvoiceDrafts.EditInvoiceDraft;

public class EditInvoiceDraftCommandModel
{
    public int Id { get; set; }

    [RegularExpression(Pattern.Common.SomeContent)]
    [Required(ErrorMessage = "Ange sammanfattning.")]
    [StringLength(
        MaxLengths.Common.Invoice.About,
        ErrorMessage = "Skriv kortare.")]
    public string? About { get; set; }
}
