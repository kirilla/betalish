namespace Betalish.Domain.Entities;

public class CustomerMessage : ICreatedDateTime
{
    public int Id { get; set; }

    public CustomerMessageKind CustomerMessageKind { get; set; }

    public DateTime? Created { get; set; }

    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;
}
