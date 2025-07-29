namespace Betalish.Application.Routines.UpdateInvoiceAccounting;

public class UpdateInvoiceAccountingRoutine(
    IDatabaseService database) : IUpdateInvoiceAccountingRoutine
{
    public async Task Execute(
        IUserToken userToken, int invoiceId)
    {
        database.ChangeTracker.Clear();

        var invoice = await database.Invoices
            .AsNoTracking()
            .Where(x =>
                x.Id == invoiceId &&
                x.ClientId == userToken.ClientId!.Value)
            .SingleOrDefaultAsync();

        if (invoice == null)
            return;

        var accountingsToRemove = await database.InvoiceAccountings
            .Where(x => 
                x.InvoiceId == invoice.Id &&
                x.Invoice.ClientId == userToken.ClientId!.Value)
            .ToListAsync();

        database.InvoiceAccountings.RemoveRange(accountingsToRemove);

        var invoiceRows = await database.InvoiceRows
            .AsNoTracking()
            .Where(x =>
                x.InvoiceId == invoice.Id &&
                x.Invoice.ClientId == userToken.ClientId!.Value)
            .ToListAsync();

        var receivableRow = new InvoiceAccounting()
        {
            Account = Defaults.Accounting.AccountsReceivable,
            Debit = invoice.Total,
            Credit = 0,
            InvoiceId = invoice.Id,
        };

        database.InvoiceAccountings.Add(receivableRow);

        if (invoice.TotalRounding != 0)
        {
            var roundingRow = new InvoiceAccounting()
            {
                Account = Defaults.Accounting.Rounding,
                Debit = 0,
                Credit = invoice.TotalRounding,
                InvoiceId = invoice.Id,
            };

            database.InvoiceAccountings.Add(roundingRow);
        }

        var revenueCreditRows = invoiceRows
            .GroupBy(x => new {
                x.RevenueAccount,
            })
            .Select(x => new InvoiceAccounting()
            {
                Debit = 0,
                Credit = x.Sum(y => y.NetAmount),
                Account = x.Key.RevenueAccount,
                InvoiceId = invoice.Id,
            })
            .ToList();

        var vatCreditRows = invoiceRows
            .Where(x => x.VatAmount != 0)
            .GroupBy(x => new {
                x.VatAccount,
            })
            .Select(x => new InvoiceAccounting()
            {
                Debit = 0,
                Credit = x.Sum(y => y.VatAmount),
                Account = x.Key.VatAccount! ?? "WUT?",
                InvoiceId = invoice.Id,
            })
            .ToList();

        var list = new List<InvoiceAccounting>();

        list.AddRange(revenueCreditRows);
        list.AddRange(vatCreditRows);

        var summedAccountings = list
            .GroupBy(x => x.Account)
            .Select(x => new InvoiceAccounting()
            {
                Account = x.Key,
                Debit = x.Sum(y => y.Debit),
                Credit = x.Sum(y => y.Credit),
                InvoiceId = invoice.Id,
            })
            .ToList();

        database.InvoiceAccountings.AddRange(summedAccountings);

        await database.SaveAsync(userToken);
    }
}
