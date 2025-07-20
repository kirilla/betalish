namespace Betalish.Application.Commands.InvoiceRanges.AddInvoiceRange;

public interface IAddInvoiceRangeCommand
{
    Task<int> Execute(IUserToken userToken, AddInvoiceRangeCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
