namespace Betalish.Application.Reports.UnpaidReports;

public class UnpaidReport(IDatabaseService database) : IUnpaidReport
{
    public async Task<UnpaidReportResultsModel> Execute(
        IUserToken userToken, UnpaidReportQueryModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var reportDate = model.ReportDate?.ToIso8601DateOnly();

        if (reportDate == null)
            throw new InvalidDateException();

        var invoices = await database.Invoices
            .AsNoTracking()
            .Where(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.InvoiceDate <= reportDate.Value &&
                x.InvoiceStatus == InvoiceStatus.Issued)
            .OrderBy(x => x.InvoiceNumber)
            .Select(x => new UnpaidInvoice
            {
                Id = x.Id,
                InvoiceNumber = x.InvoiceNumber,
                IsCredit = x.IsCredit,
                About = x.About,
                InvoiceDate = x.InvoiceDate,
                Total = x.Total,
                Balance = x.Balance,
                LeftToPay = x.LeftToPay,
                Customer_Name = x.Customer_Name,
            })
            .ToListAsync();

        var invoiceFees = await database.InvoiceFees
            .AsNoTracking()
            .Where(x => 
                x.Invoice.ClientId == userToken.ClientId!.Value &&
                x.Date <= reportDate.Value)
            .ToListAsync();

        var payments = await database.Payments
            .AsNoTracking()
            .Where(x => 
                x.Invoice!.ClientId == userToken.ClientId!.Value &&
                x.Date <= reportDate.Value)
            .ToListAsync();

        var debitBalanceRows = await database.BalanceRows
            .AsNoTracking()
            .Where(x => 
                x.DebitInvoice.ClientId == userToken.ClientId!.Value &&
                x.Date <= reportDate.Value)
            .ToListAsync();

        var creditBalanceRows = await database.BalanceRows
            .AsNoTracking()
            .Where(x => 
                x.CreditInvoice.ClientId == userToken.ClientId!.Value &&
                x.Date <= reportDate.Value)
            .ToListAsync();

        foreach (var invoice in invoices)
        {
            invoice.InvoiceFees = invoiceFees
                .Where(x => x.InvoiceId == invoice.Id)
                .ToList();

            invoice.Payments = payments
                .Where(x => x.InvoiceId == invoice.Id)
                .ToList();

            invoice.DebitBalanceRows = debitBalanceRows
                .Where(x => x.DebitInvoiceId == invoice.Id)
                .ToList();

            invoice.CreditBalanceRows = creditBalanceRows
                .Where(x => x.CreditInvoiceId == invoice.Id)
                .ToList();
        }

        foreach (var invoice in invoices)
        {
            invoice.UpdatePaymentStatus();
        }

        invoices = invoices
            .Where(x => x.LeftToPay > 0)
            .ToList();

        var total = invoices.Sum(x => x.Total);
        var leftToPay = invoices.Sum(x => x.LeftToPay);

        return new UnpaidReportResultsModel()
        {
            Invoices = invoices,
            Total = total,
            LeftToPay = leftToPay,
        };
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
