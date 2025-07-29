using Betalish.Application.Routines.UpdateInvoicePaymentStatus;

namespace Betalish.Application.Commands.Payments.UnassignPayment;

public class UnassignPaymentCommand(
    IDatabaseService database,
    IUpdateInvoicePaymentStatusRoutine updateInvoicePaymentStatus) : IUnassignPaymentCommand
{
    public async Task Execute(
        IUserToken userToken, UnassignPaymentCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var payment = await database.Payments
            .Where(x =>
                x.Id == model.Id &&
                x.ClientId == userToken.ClientId!.Value)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        if (payment.InvoiceId == null)
            throw new NotAssignedException();

        // TODO: Asserts

        // If debit, if credit, ...?

        var invoiceId = payment.InvoiceId!.Value;

        payment.InvoiceId = null;
        payment.InvoiceNumber = null;

        var paymentAccountingRows = await database.PaymentAccountingRows
            .Where(x => 
                x.PaymentId == payment.Id &&
                x.Payment.ClientId == userToken.ClientId!.Value)
            .ToListAsync();

        database.PaymentAccountingRows.RemoveRange(paymentAccountingRows);

        await database.SaveAsync(userToken);

        await updateInvoicePaymentStatus.Execute(userToken, invoiceId);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
