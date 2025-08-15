namespace Betalish.Application.Routines.ScheduleInvoice;

public interface IScheduleInvoiceRoutine
{
    Task Execute(IUserToken userToken, int invoiceId, int? paymentTermsId);
}
