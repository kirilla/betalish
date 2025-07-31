using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Reports.InvoiceReports;

public class InvoiceReport(IDatabaseService database) : IInvoiceReport
{
    public async Task<InvoiceReportResultsModel> Execute(
        IUserToken userToken, InvoiceReportQueryModel model)
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

        var ledgerAccounts = await database.LedgerAccounts
            .AsNoTracking()
            .Where(x => x.ClientId == userToken.ClientId!.Value)
            .ToListAsync();

        var articles = await database.Articles
            .AsNoTracking()
            .Where(x => x.ClientId == userToken.ClientId!.Value)
            .ToListAsync();

        var invoices = await database.Invoices
            .AsNoTracking()
            .Where(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.InvoiceDate >= startDate.Value &&
                x.InvoiceDate <= endDate.Value && 
                x.InvoiceStatus == InvoiceStatus.Issued)
            .OrderBy(x => x.InvoiceNumber)
            .ToListAsync();

        var invoiceRows = await database.InvoiceRows
            .AsNoTracking()
            .Where(x =>
                x.Invoice.ClientId == userToken.ClientId!.Value &&
                x.Invoice.InvoiceDate >= startDate.Value &&
                x.Invoice.InvoiceDate <= endDate.Value &&
                x.Invoice.InvoiceStatus == InvoiceStatus.Issued)
            .OrderBy(x => x.Invoice.InvoiceNumber)
            .ToListAsync();

        var accountingRows = await database.InvoiceAccountingRows
            .AsNoTracking()
            .Where(x =>
                x.Invoice.ClientId == userToken.ClientId!.Value &&
                x.Invoice.InvoiceDate >= startDate.Value &&
                x.Invoice.InvoiceDate <= endDate.Value &&
                x.Invoice.InvoiceStatus == InvoiceStatus.Issued)
            .OrderBy(x => x.Invoice.InvoiceNumber)
            .ToListAsync();

        // Generate
        var summedInvoiceRows = invoiceRows
            .GroupBy(x => new
            {
                x.ArticleNumber,
                x.ArticleName,
            })
            .ToList()
            .Select(x => new SummedInvoiceRow()
            {
                ArticleNumber = x.Key.ArticleNumber,
                ArticleName = x.Key.ArticleName,
                NetAmount = x.Sum(y => y.NetAmount),
                Quantity = x.Sum(y => 
                    y.IsCredit ?
                    -y.Quantity : y.Quantity),
            })
            .Where(x => 
                x.NetAmount != 0 && 
                x.Quantity != 0)
            .ToList();

        var summedAccountingRows = accountingRows
            .GroupBy(x => new { 
                x.Account,
            })
            .Select(x => new SummedInvoiceAccountingRow()
            {
                Account = x.Key.Account,
                Debit = x.Sum(y => y.Debit),
                Credit = x.Sum(y => y.Credit),
            })
            .OrderBy(x => x.Account)
            .ToList();

        foreach (var row in summedAccountingRows)
        {
            row.Normalize();

            row.Description = ledgerAccounts
                .Where(y => y.Account == row.Account)
                .Select(y => y.Description)
                .SingleOrDefault() ?? 
                "Kontot finns inte i kontoplanen.";
        }

        summedAccountingRows = summedAccountingRows
            .Where(x => !x.IsEmpty())
            .ToList();

        return new InvoiceReportResultsModel()
        {
            SummedInvoiceAccountingRows = summedAccountingRows,
            SummedInvoiceRows = summedInvoiceRows,
        };
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
