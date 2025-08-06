namespace Betalish.Application.Routines.CreateInvoicePlan.Debit;

public interface ICreateDebitInvoicePlanRoutine
{
    Task Execute(IUserToken userToken, int invoiceId, int? paymentTermsId);
}
