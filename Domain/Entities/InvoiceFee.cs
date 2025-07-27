namespace Betalish.Domain.Entities;

public class InvoiceFee : 
    IValidateOnSave, 
    ICreatedDateTime,
    IUpdatedDateTime
{
    public int Id { get; set; }

    public required DateOnly Date { get; set; }

    public required decimal Amount { get; set; }

    public required InvoiceFeeKind InvoiceFeeKind { get; set; }

    public int InvoiceId { get; set; }
    public Invoice Invoice { get; set; } = null!;

    public DateTime? Created { get; set; }
    public DateTime? Updated { get; set; }

    public void ValidateOnSave()
    {
        if (!Enum.IsDefined(InvoiceFeeKind))
            throw new InvalidEnumException();
    }
}
