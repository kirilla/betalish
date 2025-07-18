namespace Betalish.Application.Commands.InvoiceTemplates.RemoveInvoiceTemplate;

public interface IRemoveInvoiceTemplateCommand
{
    Task Execute(IUserToken userToken, RemoveInvoiceTemplateCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
