namespace Betalish.Domain.Entities;

public class CustomerMessage : 
    ICreatedDateTime,
    IUpdatedDateTime,
    IValidateOnSave
{
    public int Id { get; set; }

    public CustomerMessageKind MessageKind { get; set; }
    public CustomerMessageDeliveryStatus DeliveryStatus { get; set; }
    public CustomerMessageDistributionMethod DistributionMethod { get; set; }

    public DateTime? Created { get; set; }
    public DateTime? Updated { get; set; }

    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;

    public int? InvoiceId { get; set; }
    public Invoice? Invoice { get; set; }
    
    public void ValidateOnSave()
    {
        if (Enum.IsDefined(MessageKind))
            throw new InvalidEnumException(
                "CustomerMessageKind");

        if (Enum.IsDefined(DeliveryStatus))
            throw new InvalidEnumException(
                "CustomerMessageDeliveryStatus");

        if (Enum.IsDefined(DistributionMethod))
            throw new InvalidEnumException(
                "CustomerMessageDistributionMethod");
    }
}
