namespace Betalish.Application.Commands.InvoiceDraftRows.EditInvoiceDraftRow;

public class EditInvoiceDraftRowCommand(IDatabaseService database) : IEditInvoiceDraftRowCommand
{
    public async Task Execute(
        IUserToken userToken, EditInvoiceDraftRowCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var row = await database.InvoiceDraftRows
            .Where(x =>
                x.InvoiceDraft.ClientId == userToken.ClientId!.Value &&
                x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        row.ArticleName = model.ArticleName!;
        row.Quantity = model.Quantity!.TryParseDecimal()!.Value;
        row.UnitPrice = model.UnitPrice!.TryParseDecimal()!.Value;
        row.RevenueAccount = model.RevenueAccount!;
        row.VatAccount = model.VatAccount!;
        
        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
