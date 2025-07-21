namespace Betalish.Application.Commands.InvoiceDrafts.ApproveInvoiceDraft;

public interface IApproveInvoiceDraftCommand
{
    Task Execute(IUserToken userToken, ApproveInvoiceDraftCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
