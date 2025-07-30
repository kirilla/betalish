namespace Betalish.Domain.Entities;

public class BalanceRow : 
    ICreatedDateTime, 
    IUpdatedDateTime,
    IValidateOnSave
{
    public int Id { get; set; }

    public required Guid? RefGuid { get; set; }
    public required int? RefInvoiceNumber { get; set; }
    
    public required decimal Amount { get; set; }

    public required DateOnly Date { get; set; }

    public DateTime? Created { get; set; }
    public DateTime? Updated { get; set; }

    public int InvoiceId { get; set; }
    public Invoice Invoice { get; set; } = null!;

    public void ValidateOnSave()
    {
        RefGuid.AssertValid();
    }
}
