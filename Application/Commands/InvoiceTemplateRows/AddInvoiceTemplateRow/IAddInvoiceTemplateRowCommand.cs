namespace Betalish.Application.Commands.InvoiceTemplateRows.AddInvoiceTemplateRow;

public interface IAddInvoiceTemplateRowCommand
{
    Task Execute(IUserToken userToken, AddInvoiceTemplateRowCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
