namespace Betalish.Application.Commands.InvoiceRanges.RemoveInvoiceRange;

public interface IRemoveInvoiceRangeCommand
{
    Task Execute(IUserToken userToken, RemoveInvoiceRangeCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
