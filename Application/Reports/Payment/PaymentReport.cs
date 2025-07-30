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

        if (startDate >= endDate)
            throw new InvalidDateException();

        var payments = await database.Payments
            .AsNoTracking()
            .Where(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.Date >= startDate.Value &&
                x.Date <= endDate.Value)
            .ToListAsync();

        var paymentAccountingRows = await database.PaymentAccountingRows
            .AsNoTracking()
            .Where(x =>
                x.Payment.ClientId == userToken.ClientId!.Value &&
                x.Payment.Date >= startDate.Value &&
                x.Payment.Date <= endDate.Value)
            .ToListAsync();

        return new PaymentReportResultsModel()
        {
            Payments = payments,
            PaymentAccountingRows = paymentAccountingRows,
        };
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
