using Betalish.Application.Routines.SendInvoiceEmail;
using Betalish.Application.Routines.UpdateInvoiceAccountingRows;
using Betalish.Application.Routines.UpdateInvoicePaymentStatus;

namespace Betalish.Application.Commands.Invoices.ExecuteInvoiceRoutine;

public class ExecuteInvoiceRoutineCommand(
    IDatabaseService database,
    ISendInvoiceEmailRoutine sendInvoiceEmail,
    IUpdateInvoiceAccountingRowsRoutine updateAccountingRows,
    IUpdateInvoicePaymentStatusRoutine updatePaymentStatus) : IExecuteInvoiceRoutineCommand
{
    public async Task Execute(
        IUserToken userToken, ExecuteInvoiceRoutineCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        var invoice = await database.Invoices
            .AsNoTracking()
            .Where(x =>
                x.Id == model.InvoiceId &&
                x.ClientId == userToken.ClientId!.Value)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        if (model.SendInvoiceEmail)
            await sendInvoiceEmail.Execute(userToken, invoice.Id);

        if (model.UpdateInvoiceAccountingRows)
            await updateAccountingRows.Execute(userToken, invoice.Id);

        if (model.UpdateInvoicePaymentStatus)
            await updatePaymentStatus.Execute(userToken, invoice.Id);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient && // NOTE: Both
            userToken.IsAdmin;
    }
}
