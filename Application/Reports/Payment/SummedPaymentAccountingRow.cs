namespace Betalish.Application.Reports.Payment;

public class SummedPaymentAccountingRow
{
    public required string Account { get; set; }

    public required decimal Debit { get; set; }
    public required decimal Credit { get; set; }

    public string Description { get; set; }

    public void Normalize()
    {
        RaiseToZero();
        LowerToZero();
    }

    public void RaiseToZero()
    {
        decimal correction = 0;

        if (Debit < 0)
            correction = Math.Abs(Debit);

        if (Credit < 0)
            correction = Math.Abs(Credit);

        Debit += correction;
        Credit += correction;
    }

    public void LowerToZero()
    {
        if (Debit == Credit)
        {
            Debit = 0;
            Credit = 0;
        }

        if (Debit > 0 && Credit > 0)
        {
            decimal lesserAmount = 0;

            if (Debit < Credit)
                lesserAmount = Debit;

            if (Credit < Debit)
                lesserAmount = Credit;

            Debit -= lesserAmount;
            Credit -= lesserAmount;
        }
    }
}
