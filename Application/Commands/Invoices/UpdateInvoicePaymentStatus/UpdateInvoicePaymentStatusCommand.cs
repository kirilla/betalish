using Betalish.Application.Routines.UpdateInvoicePaymentStatus;

namespace Betalish.Application.Commands.Invoices.UpdateInvoicePaymentStatus;

public class UpdateInvoicePaymentStatusCommand(
    IDatabaseService database,
    IUpdateInvoicePaymentStatusRoutine updatePaymentStatus) : IUpdateInvoicePaymentStatusCommand
{
    public async Task Execute(
        IUserToken userToken, UpdateInvoicePaymentStatusCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var invoice = await database.Invoices
            .AsNoTracking()
            .Where(x =>
                x.Id == model.InvoiceId &&
                x.ClientId == userToken.ClientId!.Value)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        await updatePaymentStatus.Execute(userToken, invoice.Id);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient && // NOTE: Both
            userToken.IsAdmin;
    }
}
