namespace Betalish.Application.Commands.InvoiceTemplates.EditInvoiceTemplate;

public interface IEditInvoiceTemplateCommand
{
    Task Execute(IUserToken userToken, EditInvoiceTemplateCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
