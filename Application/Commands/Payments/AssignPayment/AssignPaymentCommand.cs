using Betalish.Application.Routines.UpdateInvoicePaymentStatus;

namespace Betalish.Application.Commands.Payments.AssignPayment;

public class AssignPaymentCommand(
    IDatabaseService database,
    IUpdateInvoicePaymentStatusRoutine updatePaymentStatus) : IAssignPaymentCommand
{
    public async Task Execute(
        IUserToken userToken, AssignPaymentCommandModel model)
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

        var invoice = await database.Invoices
            .Where(x =>
                x.Id == model.InvoiceId!.Value &&
                x.ClientId == userToken.ClientId!.Value)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        if (payment.InvoiceId.HasValue)
            throw new AlreadyAssignedException();

        // TODO: More asserts

        // If debit, if credit, ...?

        payment.InvoiceId = invoice.Id;
        payment.InvoiceNumber = invoice.InvoiceNumber;

        await database.SaveAsync(userToken);

        await updatePaymentStatus.Execute(userToken, invoice.Id);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
