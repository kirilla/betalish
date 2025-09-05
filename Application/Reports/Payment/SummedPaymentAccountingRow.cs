namespace Betalish.Application.Reports.Payment;

public class SummedPaymentAccountingRow
{
    public required string Account { get; set; }

    public required decimal Debit { get; set; }
    public required decimal Credit { get; set; }

    public string? Description { get; set; }

    public void Normalize()
    {
        (Debit, Credit) = AccountingLogic.Normalize(Debit, Credit);
    }
}
