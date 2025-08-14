namespace Betalish.Domain.Entities;

public class DistributionMessage : 
    IValidateOnSave, 
    ICreatedDateTime
{
    public int Id { get; set; }

    public required DistributionMessageKind DistributionMessageKind { get; set; }
    public required DistributionStatus DistributionStatus { get; set; }

    public DateTime? Created { get; set; }
    public DateTime? Distributed { get; set; }

    public int InvoiceId { get; set; }
    public Invoice Invoice { get; set; } = null!;

    public void ValidateOnSave()
    {
        if (!Enum.IsDefined(DistributionMessageKind))
            throw new InvalidEnumException(
                nameof(DistributionMessageKind));

        if (!Enum.IsDefined(DistributionStatus))
            throw new InvalidEnumException(
                nameof(DistributionStatus));
    }
}
