namespace Betalish.Application.Routines.SetInvoiceNumber;

public class SetInvoiceNumberRoutine(
    IDatabaseService database) : ISetInvoiceNumberRoutine
{
    public async Task Execute(
        IUserToken userToken, int invoiceId)
    {
        database.ChangeTracker.Clear();

        var invoice = await database.Invoices
            .Where(x =>
                x.Id == invoiceId &&
                x.ClientId == userToken.ClientId!.Value && 
                x.InvoiceStatus == InvoiceStatus.Draft &&
                x.InvoiceNumber == null)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        await InvoiceNumberAsyncLock.ExecuteAsync(async () =>
        {
            var ranges = await database.InvoiceRanges
                .AsNoTracking()
                .Where(x => x.ClientId == userToken.ClientId!.Value)
                .ToListAsync();

            // TODO: A global lock, a per-client lock ?

            // TODO:
            //
            // 1. Find the InvoiceDate of the invoice.
            // 2. Find the range for the invoice date. 
            // 3. Find all invoice numbers within the range.
            // 4. Find the top invoice number
            // 5. Increment +1
            // 6. Assert still within range.
        });

        throw new NotImplementedException();

        invoice.InvoiceStatus = InvoiceStatus.Issued;

        await database.SaveAsync(userToken);
    }
}
