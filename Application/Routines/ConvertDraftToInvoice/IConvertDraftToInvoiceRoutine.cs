namespace Betalish.Application.Routines.ConvertDraftToInvoice;

public interface IConvertDraftToInvoiceRoutine
{
    Task<int> Execute(IUserToken userToken, int invoiceDraftId);
}
