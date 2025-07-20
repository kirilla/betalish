namespace Betalish.Application.Commands.InvoiceRanges.RemoveInvoiceRange;

public class RemoveInvoiceRangeCommand(IDatabaseService database) : IRemoveInvoiceRangeCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveInvoiceRangeCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var range = await database.InvoiceRanges
            .Where(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.InvoiceRanges.Remove(range);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
