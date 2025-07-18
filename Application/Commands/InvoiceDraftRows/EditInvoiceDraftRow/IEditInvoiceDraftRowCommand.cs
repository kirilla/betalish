namespace Betalish.Application.Commands.InvoiceDraftRows.EditInvoiceDraftRow;

public interface IEditInvoiceDraftRowCommand
{
    Task Execute(IUserToken userToken, EditInvoiceDraftRowCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
