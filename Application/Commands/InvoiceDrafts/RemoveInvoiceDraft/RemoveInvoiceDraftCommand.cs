namespace Betalish.Application.Commands.InvoiceDrafts.RemoveInvoiceDraft;

public class RemoveInvoiceDraftCommand(IDatabaseService database) : IRemoveInvoiceDraftCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveInvoiceDraftCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var draft = await database.InvoiceDrafts
            .Where(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.InvoiceDrafts.Remove(draft);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
