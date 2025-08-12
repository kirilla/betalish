namespace Betalish.Application.Commands.Invoices.ExecuteInvoiceRoutine;

public class ExecuteInvoiceRoutineCommandModel
{
    public int InvoiceId { get; set; }

    public bool UpdateInvoiceAccountingRows { get; set; }
    public bool UpdateInvoicePaymentStatus { get; set; }
}
