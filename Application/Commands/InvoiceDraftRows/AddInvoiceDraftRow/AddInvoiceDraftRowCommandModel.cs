using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.InvoiceDraftRows.AddInvoiceDraftRow;

public class AddInvoiceDraftRowCommandModel
{
    public int InvoiceDraftId { get; set; }

    [Required(ErrorMessage = "Ange artikel.")]
    public int? ArticleId { get; set; }

    // TODO: Quantity

    // TODO: UnitPrice
    // for per-customer changes at draft-time
}
