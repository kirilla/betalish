namespace Betalish.Application.Commands.InvoiceTemplateRows.RemoveInvoiceTemplateRow;

public class RemoveInvoiceTemplateRowCommand(IDatabaseService database) : IRemoveInvoiceTemplateRowCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveInvoiceTemplateRowCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var row = await database.InvoiceTemplateRows
            .Where(x =>
                x.InvoiceTemplate.ClientId == userToken.ClientId!.Value &&
                x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.InvoiceTemplateRows.Remove(row);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
