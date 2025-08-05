using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.InvoiceDrafts.AddInvoiceDraft;

public class AddInvoiceDraftCommandModel
{
    [Required(ErrorMessage = "Ange kund.")]
    public int? CustomerId { get; set; }

    public int? InvoiceTemplateId { get; set; }

    [RegularExpression(Pattern.Common.SomeContent)]
    [Required(ErrorMessage = "Ange sammanfattning.")]
    [StringLength(
        MaxLengths.Common.Invoice.About,
        ErrorMessage = "Skriv kortare.")]
    public string? About { get; set; }

    [Required(ErrorMessage = "Ange betalvillkor.")]
    public int? PaymentTermsId { get; set; }
}
