namespace Betalish.Application.Commands.InvoiceDrafts.EditInvoiceDraft;

public interface IEditInvoiceDraftCommand
{
    Task Execute(IUserToken userToken, EditInvoiceDraftCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
