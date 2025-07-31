using Betalish.Application.Queues.LogItems;

namespace Betalish.Application.Routines.SetBalanceRowCreditInvoiceNumber;

public class SetBalanceRowCreditInvoiceNumberRoutine(
    IDatabaseService database,
    ILogItemList logItemList) : ISetBalanceRowCreditInvoiceNumberRoutine
{
    public async Task Execute(
        IUserToken userToken, int creditInvoiceId)
    {
        database.ChangeTracker.Clear();

        var creditInvoice = await database.Invoices
            .Where(x => 
                x.Id ==  creditInvoiceId &&
                x.ClientId == userToken.ClientId &&
                x.IsCredit == true &&
                x.InvoiceNumber != null)
            .SingleOrDefaultAsync() ?? 
            throw new RoutineException(
                nameof(SetBalanceRowCreditInvoiceNumberRoutine));

        var balanceRows = await database.BalanceRows
            .Where(x =>
                x.CreditInvoiceId == creditInvoice!.Id &&
                x.CreditInvoice.ClientId == userToken.ClientId!.Value)
            .ToListAsync();

        foreach (var row in balanceRows)
        {
            row.CreditInvoiceNumber = creditInvoice.InvoiceNumber;
        }

        await database.SaveAsync(userToken);
    }
}
