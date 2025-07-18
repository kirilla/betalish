namespace Betalish.Application.Commands.InvoiceTemplateRows.EditInvoiceTemplateRow;

public interface IEditInvoiceTemplateRowCommand
{
    Task Execute(IUserToken userToken, EditInvoiceTemplateRowCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
