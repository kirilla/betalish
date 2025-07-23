using Betalish.Application.Queues.LogItems;

namespace Betalish.Application.Routines.SetInvoiceNumber;

public class SetInvoiceNumberRoutine(
    IDatabaseService database,
    ILogItemList logItemList) : ISetInvoiceNumberRoutine
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
            try
            {
                var ranges = await database.InvoiceRanges
                    .AsNoTracking()
                    .Where(x => x.ClientId == userToken.ClientId!.Value)
                    .ToListAsync();

                // Find the InvoiceDate
                var invoiceDate = invoice.InvoiceDate;

                // Find the InvoiceRange for the date
                var range = ranges.Where(x =>
                    x.EffectiveStartDate <= invoiceDate &&
                    x.EffectiveEndDate >= invoiceDate)
                    .SingleOrDefault() ??
                    throw new NotFoundException(
                        $"No suitable InvoiceRange for invoice " +
                        $"'{invoice.Id}', client '{userToken.ClientName}'.");

                // Find the top invoice number used, within the range
                int highestTakenNumber = await database.Invoices
                    .Where(x =>
                        x.ClientId == invoice.ClientId &&
                        x.InvoiceNumber >= range.StartNumber &&
                        x.InvoiceNumber <= range.EndNumber)
                    .Select(x => x.InvoiceNumber)
                    .Where(x => x != null)
                    .OrderByDescending(x => x)
                    .Cast<int>()
                    .Take(1)
                    .SingleOrDefaultAsync();

                // Increment
                int nextNumber = highestTakenNumber + 1;

                // Assert still within range
                if (nextNumber < range.StartNumber ||
                    nextNumber > range.EndNumber)
                    throw new OutOfRangeException(
                        $"Invoice: {invoice.Id}, InvoiceRange: {range.Id}.");

                invoice.InvoiceNumber = nextNumber;
                invoice.InvoiceStatus = InvoiceStatus.Issued;

                await database.SaveAsync(userToken);
            }
            catch (Exception ex)
            {
                logItemList.AddLogItem(new LogItem(ex)
                {
                    LogItemKind = LogItemKind.SetInvoiceNumberRoutineFailed,
                });
            }
        });
    }
}
