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
        (Debit, Credit) = AccountingLogic.Normalize(Debit, Credit);
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
}
