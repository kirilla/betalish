namespace Betalish.Application.Commands.InvoiceDrafts.AddInvoiceDraft;

public interface IAddInvoiceDraftCommand
{
    Task<int> Execute(IUserToken userToken, AddInvoiceDraftCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
