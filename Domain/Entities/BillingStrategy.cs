namespace Betalish.Domain.Entities;

public class BillingStrategy
{
    public int Id { get; set; }

    public required string Name { get; set; }

    // Relations
    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;
}
