namespace Betalish.Domain.Entities;

public class Client
{
    public int Id { get; set; }

    public string Name { get; set; }
    public string Address { get; set; }

    public List<ClientAuth> ClientAuths { get; set; }
    public List<ClientEmailAccount> ClientEmailAccounts { get; set; }
    public List<ClientEmailMessage> ClientEmailMessages { get; set; }
    public List<Customer> Customers { get; set; }
}
