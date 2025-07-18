namespace Betalish.Application.Commands.InvoiceTemplateRows.EditInvoiceTemplateRow;

public class EditInvoiceTemplateRowCommand(IDatabaseService database) : IEditInvoiceTemplateRowCommand
{
    public async Task Execute(
        IUserToken userToken, EditInvoiceTemplateRowCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var row = await database.InvoiceTemplateRows
            .Where(x =>
                x.InvoiceTemplate.ClientId == userToken.ClientId!.Value &&
                x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        row.Quantity = model.Quantity!.TryParseDecimal()!.Value;

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
