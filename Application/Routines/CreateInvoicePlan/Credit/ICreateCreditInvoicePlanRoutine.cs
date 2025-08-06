namespace Betalish.Application.Routines.CreateInvoicePlan.Credit;

public interface ICreateCreditInvoicePlanRoutine
{
    Task Execute(IUserToken userToken, int invoiceId);
}
