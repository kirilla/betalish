using Betalish.Application.Routines.UpdateInvoiceAccounting;

namespace Betalish.Application.Commands.Invoices.UpdateInvoiceAccounting;

public class UpdateInvoiceAccountingCommand(
    IDatabaseService database,
    IUpdateInvoiceAccountingRoutine updateSummaryRoutine) : IUpdateInvoiceAccountingCommand
{
    public async Task Execute(
        IUserToken userToken, UpdateInvoiceAccountingCommandModel model)
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
                x.Id == model.Id &&
                x.ClientId == userToken.ClientId!.Value)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        await updateSummaryRoutine.Execute(userToken, invoice.Id);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
