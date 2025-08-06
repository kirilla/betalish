namespace Betalish.Application.Routines.CreateInvoicePlan;

public interface ICreateInvoicePlanRoutine
{
    Task Execute(IUserToken userToken, int invoiceId, int? paymentTermsId);
}
