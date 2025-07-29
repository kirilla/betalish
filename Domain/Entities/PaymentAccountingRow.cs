namespace Betalish.Domain.Entities;

public class PaymentAccountingRow : 
    IFormatOnSave, 
    IValidateOnSave
{
    public int Id { get; set; }

    public required string Account { get; set; }

    public required decimal Debit { get; set; }
    public required decimal Credit { get; set; }

    public int PaymentId { get; set; }
    public Payment Payment { get; set; } = null!;

    public void FormatOnSave()
    {
        RaiseToZero();
        LowerToZero();
    }

    public void ValidateOnSave()
    {
        if ((Debit > 0 && Credit == 0) || 
            (Debit == 0 && Credit > 0))
        {
            // allow
        }
        else
        {
            throw new ValidateOnSaveException(
                $"Ogiltigt kontering. Debet: {Debit}, Credit: {Credit}.");
        }
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
