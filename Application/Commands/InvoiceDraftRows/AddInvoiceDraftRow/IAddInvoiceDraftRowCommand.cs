namespace Betalish.Application.Commands.InvoiceDraftRows.AddInvoiceDraftRow;

public interface IAddInvoiceDraftRowCommand
{
    Task Execute(IUserToken userToken, AddInvoiceDraftRowCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
