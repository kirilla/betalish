namespace Betalish.Domain.Entities;

public class PaymentAccount
{
    public int Id { get; set; }

    public required string Name { get; set; }
    public required string? Description { get; set; }

    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;
}
