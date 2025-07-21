using Betalish.Application.Routines.UpdateInvoiceDraftSummary;

namespace Betalish.Application.Commands.InvoiceDraftRows.RemoveInvoiceDraftRow;

public class RemoveInvoiceDraftRowCommand(
    IDatabaseService database,
    IUpdateInvoiceDraftSummaryRoutine updateSummaryRoutine) : IRemoveInvoiceDraftRowCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveInvoiceDraftRowCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var row = await database.InvoiceDraftRows
            .Where(x =>
                x.InvoiceDraft.ClientId == userToken.ClientId!.Value &&
                x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        var draftId = row.InvoiceDraftId;

        database.InvoiceDraftRows.Remove(row);
        
        await database.SaveAsync(userToken);

        await updateSummaryRoutine.Execute(userToken, draftId);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
