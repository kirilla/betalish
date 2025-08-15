namespace Betalish.Application.Commands.Invoices.ExecuteInvoiceRoutine;

public class ExecuteInvoiceRoutineCommandModel
{
    public int InvoiceId { get; set; }

    public bool UpdateInvoiceAccountingRows { get; set; }
    public bool UpdateInvoicePaymentStatus { get; set; }

    public bool SendInvoiceEmail { get; set; }
    public bool SendReminderEmail { get; set; }
    public bool SendDemandEmail { get; set; }
    public bool SendCollectEmail { get; set; }
}
