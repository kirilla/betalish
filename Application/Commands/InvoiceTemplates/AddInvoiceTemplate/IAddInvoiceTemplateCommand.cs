namespace Betalish.Application.Commands.InvoiceTemplates.AddInvoiceTemplate;

public interface IAddInvoiceTemplateCommand
{
    Task<int> Execute(IUserToken userToken, AddInvoiceTemplateCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
