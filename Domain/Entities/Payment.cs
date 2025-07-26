namespace Betalish.Domain.Entities;

public class Payment
{
    public int Id { get; set; }

    public required decimal Amount { get; set; }

    public int? InvoiceId { get; set; }
    public Invoice? Invoice { get; set; } = null!;

    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;
}
