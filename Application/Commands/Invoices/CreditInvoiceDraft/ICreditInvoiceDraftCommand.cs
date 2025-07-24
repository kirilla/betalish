namespace Betalish.Application.Commands.Invoices.CreditInvoiceDraft;

public interface ICreditInvoiceDraftCommand
{
    Task<int> Execute(IUserToken userToken, CreditInvoiceDraftCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
