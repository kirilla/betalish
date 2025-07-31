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
            //.Include(x => x.Payments)
            .Where(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.InvoiceDate <= reportDate.Value &&
                x.InvoiceStatus == InvoiceStatus.Issued)
            .OrderBy(x => x.InvoiceNumber)
            .ToListAsync();

        foreach (var invoice in invoices)
        {
            // TODO:
            //
            // Calculate LeftToPay __at report date__ 
            // 
            // Base on the Payment dates.
        }

        // TEMP:

        invoices = invoices
            .Where(x => x.LeftToPay > 0)
            .ToList();

        var total = invoices.Sum(x => x.Total);
        var leftToPay = invoices.Sum(x => x.LeftToPay); // NOTE: Temp

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
