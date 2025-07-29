namespace Betalish.Application.Routines.UpdatePaymentAccountingRows;

public class UpdatePaymentAccountingRowsRoutine(
    IDatabaseService database) : IUpdatePaymentAccountingRowsRoutine
{
    public async Task Execute(
        IUserToken userToken, int paymentId)
    {
        database.ChangeTracker.Clear();

        var payment = await database.Payments
            .AsNoTracking()
            .Where(x =>
                x.Id == paymentId &&
                x.ClientId == userToken.ClientId!.Value)
            .SingleOrDefaultAsync() ?? 
            throw new NotFoundException();

        var paymentAccount = await database.PaymentAccounts
            .AsNoTracking()
            .Where(x =>
                x.Id == payment.PaymentAccountId &&
                x.ClientId == userToken.ClientId!.Value)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        var accountingsToRemove = await database.PaymentAccountingRows
            .Where(x => 
                x.PaymentId == payment.Id &&
                x.Payment.ClientId == userToken.ClientId!.Value)
            .ToListAsync();

        database.PaymentAccountingRows.RemoveRange(accountingsToRemove);

        /*
            Debet Bankkonto (1930) eller motsvarande
            Kredit Kundfordringar (1510)
        */

        var debitRow = new PaymentAccountingRow()
        {
            Account = paymentAccount.Account,
            Debit = payment.Amount,
            Credit = 0,
            PaymentId = payment.Id,
        };

        database.PaymentAccountingRows.Add(debitRow);

        var creditRow = new PaymentAccountingRow()
        {
            Account = Defaults.Accounting.AccountsReceivable,
            Debit = 0,
            Credit = payment.Amount,
            PaymentId = payment.Id,
        };

        database.PaymentAccountingRows.Add(creditRow);

        await database.SaveAsync(userToken);
    }
}
