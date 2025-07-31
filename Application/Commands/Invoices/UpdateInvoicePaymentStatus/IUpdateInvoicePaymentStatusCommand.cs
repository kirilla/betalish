namespace Betalish.Application.Commands.Invoices.UpdateInvoicePaymentStatus;

public interface IUpdateInvoicePaymentStatusCommand
{
    Task Execute(IUserToken userToken, UpdateInvoicePaymentStatusCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
