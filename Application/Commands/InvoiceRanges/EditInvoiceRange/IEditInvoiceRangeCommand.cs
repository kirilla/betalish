namespace Betalish.Application.Commands.InvoiceRanges.EditInvoiceRange;

public interface IEditInvoiceRangeCommand
{
    Task Execute(IUserToken userToken, EditInvoiceRangeCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
