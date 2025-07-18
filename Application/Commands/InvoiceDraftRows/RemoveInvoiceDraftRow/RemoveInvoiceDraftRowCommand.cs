namespace Betalish.Application.Commands.InvoiceDraftRows.RemoveInvoiceDraftRow;

public class RemoveInvoiceDraftRowCommand(IDatabaseService database) : IRemoveInvoiceDraftRowCommand
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

        database.InvoiceDraftRows.Remove(row);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
