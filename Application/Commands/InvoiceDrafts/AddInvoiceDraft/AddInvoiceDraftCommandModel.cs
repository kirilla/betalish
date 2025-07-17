using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.InvoiceDrafts.AddInvoiceDraft;

public class AddInvoiceDraftCommandModel
{
    [Required(ErrorMessage = "Ange kund.")]
    public int? CustomerId { get; set; }

    public int? InvoiceTemplateId { get; set; }
}
