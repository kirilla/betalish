namespace Betalish.Application.Reports.Payment;

public class PaymentReport(IDatabaseService database) : IPaymentReport
{
    public async Task<PaymentReportResultsModel> Execute(
        IUserToken userToken, PaymentReportQueryModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var startDate = model.StartDate?.ToIso8601DateOnly();
        var endDate = model.EndDate?.ToIso8601DateOnly();

        if (startDate == null || endDate == null)
            throw new InvalidDateException();

        if (startDate > endDate)
            throw new InvalidDateException();

        var payments = await database.Payments
            .AsNoTracking()
            //.Include(x => x.Invoice)
            .Include(x => x.PaymentAccount)
            .Where(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.Date >= startDate.Value &&
                x.Date <= endDate.Value)
            .OrderBy(x => x.InvoiceNumber)
            .ThenBy(x =>x.PaymentAccount!.Name)
            .ToListAsync();

        /*
        var invoices = await database.Invoices
            .AsNoTracking()
            .Where(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.Payments.Any(y => 
                    y.Date >= startDate.Value &&
                    y.Date <= endDate.Value))
            .OrderBy(x => x.InvoiceNumber)
            .ToListAsync();
        */

        var paymentDateSums = payments
            .GroupBy(x => new
            {
                x.Date,
                x.PaymentAccount!.Id,
                x.PaymentAccount.Name,
                x.PaymentAccount.Account,
            })
            .Select(x => new PaymentDateSum()
            {
                Date = x.Key.Date,
                PaymentAccountName = x.Key.Name,
                BookkeepingAccount = x.Key.Account,
                Amount = x.Sum(y => y.Amount),
                Count = x.Count(),
            })
            .Where(x => x.Amount != 0)
            .OrderBy(x => x.Date)
            .ToList();

        var paymentAccountingRows = await database.PaymentAccountingRows
            .AsNoTracking()
            .Where(x =>
                x.Payment.ClientId == userToken.ClientId!.Value &&
                x.Payment.Date >= startDate.Value &&
                x.Payment.Date <= endDate.Value)
            .ToListAsync();

        var summedPaymentAccountingRows = paymentAccountingRows
            .GroupBy(x => new
            {
                x.Account,
            })
            .Select(x => new SummedPaymentAccountingRow()
            {
                Account = x.Key.Account,
                Debit = x.Sum(y => y.Debit),
                Credit = x.Sum(y => y.Credit),
            })
            .OrderByDescending(x => x.Debit)
            .ThenByDescending(x => x.Credit)
            .ToList();

        var ledgerAccounts = await database.LedgerAccounts
            .Where(x => x.ClientId == userToken.ClientId!.Value)
            .OrderBy(x => x.Account)
            .ToListAsync();

        foreach (var row in summedPaymentAccountingRows)
        {
            row.Description = ledgerAccounts
                .Where(x => x.Account == row.Account)
                .Select(x => x.Description)
                .FirstOrDefault() ?? "-";

            row.Normalize();
        }

        return new PaymentReportResultsModel()
        {
            //Invoices = invoices,
            Payments = payments,
            PaymentAccountingRows = paymentAccountingRows,

            SummedPaymentAccountingRows = summedPaymentAccountingRows,
            PaymentDateSums = paymentDateSums,
        };
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
