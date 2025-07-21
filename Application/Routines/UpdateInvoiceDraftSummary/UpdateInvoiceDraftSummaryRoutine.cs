namespace Betalish.Application.Routines.UpdateInvoiceDraftSummary;

public class UpdateInvoiceDraftSummaryRoutine(
    IDatabaseService database) : IUpdateInvoiceDraftSummaryRoutine
{
    public async Task Execute(
        IUserToken userToken, int invoiceDraftId)
    {
        database.ChangeTracker.Clear();

        var draft = await database.InvoiceDrafts
            .Where(x =>
                x.Id == invoiceDraftId &&
                x.ClientId == userToken.ClientId!.Value)
            .SingleOrDefaultAsync();

        if (draft == null)
            return;

        /*
        var existingVatRows = await database.InvoiceDraftVatRows
            .Where(x => x.InvoiceDraftId == draft.InvoiceDraftId)
            .ToListAsync();

        database.InvoiceDraftVatRows.RemoveRange(existingVatRows);
        */

        var rows = await database.InvoiceDraftRows
            .AsNoTracking()
            .Where(x => 
                x.InvoiceDraftId == draft.Id &&
                x.InvoiceDraft.ClientId == userToken.ClientId!.Value)
            .ToListAsync();

        /*
        var groups = rows
            .GroupBy(x => new {
                //VatClass = x.VatClass ?? VatClass.Vat4,
                x.VatRate,
            })
            .ToList();

        var vatRows = groups
            .Select(x => new InvoiceDraftVatRow()
            {
                InvoiceDraftId = draft.Id,
                //VatClass = x.Key.VatClass,
                VatRate = x.Key.VatRate,
                NetAmount = x.Sum(y => y.NetAmount),
                VatAmount = x.Sum(y => y.VatAmount),
                TotalAmount =
                    x.Sum(y => y.NetAmount) +
                    x.Sum(y => y.VatAmount),
            })
            .ToList();

        database.InvoiceDraftVatRow.AddRange(vatRows);
        */

        draft.NetAmount = rows.Sum(x => x.NetAmount);
        draft.VatAmount = rows.Sum(x => x.VatAmount);

        draft.Total = rows.Sum(x => x.TotalAmount);

        draft.TotalRounding = 0;

        //if (draft.RoundTotal)
        //{
        (draft.Total, draft.TotalRounding) = MathLogic.Round(draft.Total);
        //}

        await database.SaveAsync(userToken);
    }
}
