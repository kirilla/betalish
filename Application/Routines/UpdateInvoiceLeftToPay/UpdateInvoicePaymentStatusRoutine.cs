namespace Betalish.Application.Routines.UpdateInvoicePaymentStatus;

public class UpdateInvoicePaymentStatusRoutine(
    IDatabaseService database) : IUpdateInvoicePaymentStatusRoutine
{
    public async Task Execute(
        IUserToken userToken, int invoiceId)
    {
        database.ChangeTracker.Clear();

        var invoice = await database.Invoices
            .Where(x =>
                x.Id == invoiceId &&
                x.ClientId == userToken.ClientId!.Value)
            .SingleOrDefaultAsync();

        if (invoice == null)
            return;

        var fees = await database.InvoicesFees
            .AsNoTracking()
            .Where(x =>
                x.InvoiceId == invoice.Id &&
                x.Invoice.ClientId == userToken.ClientId!.Value)
            .ToListAsync();

        var payments = await database.Payments
            .AsNoTracking()
            .Where(x =>
                x.InvoiceId == invoice.Id &&
                x.ClientId == userToken.ClientId!.Value)
            .ToListAsync();

        // TODO: Interest?

        invoice.Balance =
            (invoice.Total +
            fees.Sum(x => x.Amount) -
            payments.Sum(x => x.Amount));

        invoice.LeftToPay = decimal.Clamp(
            invoice.Balance, 0, decimal.MaxValue);

        // TODO: Different case for Credit invoice?

        // Do we need a LeftToPayBack? (Tillgodo-balance)

        await database.SaveAsync(userToken);
    }
}
