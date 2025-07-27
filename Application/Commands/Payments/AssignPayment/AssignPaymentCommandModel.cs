using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.Payments.AssignPayment;

public class AssignPaymentCommandModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Ange faktura.")]
    public int? InvoiceId { get; set; }
}
