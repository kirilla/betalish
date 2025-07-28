namespace Betalish.Domain.Entities;

public class InvoiceAccounting : IValidateOnSave
{
    public int Id { get; set; }

    public required string Account { get; set; }

    public required decimal Debit { get; set; }
    public required decimal Credit { get; set; }

    public int InvoiceId { get; set; }
    public Invoice Invoice { get; set; } = null!;

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
