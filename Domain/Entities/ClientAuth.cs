namespace Betalish.Domain.Entities;

public class ClientAuth
{
    public int Id { get; set; }

    public int ClientId { get; set; }
    public Client Client { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }
}
