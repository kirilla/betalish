namespace Betalish.Application.Commands.InvoiceDrafts.EditInvoiceDraft;

public class EditInvoiceDraftCommand(IDatabaseService database) : IEditInvoiceDraftCommand
{
    public async Task Execute(
        IUserToken userToken, EditInvoiceDraftCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var draft = await database.InvoiceDrafts
            .Where(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        draft.About = model.About!;

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
