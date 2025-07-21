namespace Betalish.Application.Routines.ConvertDraftToInvoiceRoutine;

public interface IConvertDraftToInvoiceRoutine
{
    Task<int> Execute(IUserToken userToken, int invoiceDraftId);
}
