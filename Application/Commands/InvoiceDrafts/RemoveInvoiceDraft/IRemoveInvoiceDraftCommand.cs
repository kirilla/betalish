namespace Betalish.Application.Commands.InvoiceDrafts.RemoveInvoiceDraft;

public interface IRemoveInvoiceDraftCommand
{
    Task Execute(IUserToken userToken, RemoveInvoiceDraftCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
