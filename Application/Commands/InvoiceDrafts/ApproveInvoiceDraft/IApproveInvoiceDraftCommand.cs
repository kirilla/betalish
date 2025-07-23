namespace Betalish.Application.Commands.InvoiceDrafts.ApproveInvoiceDraft;

public interface IApproveInvoiceDraftCommand
{
    Task<int> Execute(IUserToken userToken, ApproveInvoiceDraftCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
