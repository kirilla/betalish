namespace Betalish.Application.Commands.InvoiceDraftRows.RemoveInvoiceDraftRow;

public interface IRemoveInvoiceDraftRowCommand
{
    Task Execute(IUserToken userToken, RemoveInvoiceDraftRowCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
