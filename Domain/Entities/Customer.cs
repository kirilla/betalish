namespace Betalish.Domain.Entities;

public class Customer
{
    public int Id { get; set; }

    public string Name { get; set; }
    public string Address { get; set; }

    public int ClientId { get; set; }
    public Client Client { get; set; }
}
