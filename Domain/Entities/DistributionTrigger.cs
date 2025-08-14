namespace Betalish.Domain.Entities;

public class DistributionTrigger : 
    IValidateOnSave, 
    ICreatedDateTime
{
    public int Id { get; set; }

    public required DistributionTriggerKind DistributionTriggerKind { get; set; }
    public required DistributionStatus DistributionStatus { get; set; }

    public DateTime? Created { get; set; }
    public DateTime? Distributed { get; set; }

    public int InvoiceId { get; set; }
    public Invoice Invoice { get; set; } = null!;

    public void ValidateOnSave()
    {
        if (!Enum.IsDefined(DistributionTriggerKind))
            throw new InvalidEnumException(
                nameof(DistributionTriggerKind));

        if (!Enum.IsDefined(DistributionStatus))
            throw new InvalidEnumException(
                nameof(DistributionStatus));
    }
}
