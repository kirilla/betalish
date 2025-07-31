namespace Betalish.Application.Routines.SetBalanceRowCreditInvoiceNumber;

public interface ISetBalanceRowCreditInvoiceNumberRoutine
{
    Task Execute(IUserToken userToken, int creditInvoiceId);
}
