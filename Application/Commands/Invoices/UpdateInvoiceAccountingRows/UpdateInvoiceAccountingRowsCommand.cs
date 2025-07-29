using Betalish.Application.Routines.UpdateInvoiceAccountingRows;

namespace Betalish.Application.Commands.Invoices.UpdateInvoiceAccountingRows;

public class UpdateInvoiceAccountingRowsCommand(
    IDatabaseService database,
    IUpdateInvoiceAccountingRowsRoutine updateAccountingRows) : IUpdateInvoiceAccountingRowsCommand
{
    public async Task Execute(
        IUserToken userToken, UpdateInvoiceAccountingRowsCommandModel model)
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

        await updateAccountingRows.Execute(userToken, invoice.Id);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
