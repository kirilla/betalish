namespace Betalish.Application.Routines.UpdateInvoiceDraftSummary;

public interface IUpdateInvoiceDraftSummaryRoutine
{
    Task Execute(IUserToken userToken, int invoiceDraftId);
}
