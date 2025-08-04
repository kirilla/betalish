namespace Betalish.Domain.Entities;

public class BillingStrategy
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required bool Interest { get; set; }
    public required bool Reminder { get; set; }
    public required bool Demand { get; set; }
    public required bool Collect { get; set; }

    // Relations
    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;
}
