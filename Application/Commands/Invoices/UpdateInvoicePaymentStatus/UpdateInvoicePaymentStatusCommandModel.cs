namespace Betalish.Application.Commands.Invoices.UpdateInvoicePaymentStatus;

public class UpdateInvoicePaymentStatusCommandModel
{
    public int InvoiceId { get; set; }

    public bool Confirmed { get; set; }
}
