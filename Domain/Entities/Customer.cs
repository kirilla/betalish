namespace Betalish.Domain.Entities;

public class Customer : IValidateOnSave
{
    public int Id { get; set; }

    public Guid? Guid { get; set; }

    public CustomerKind CustomerKind { get; set; }

    public string Name { get; set; }

    public string? Ssn10 { get; set; }
    public string? Orgnum { get; set; }

    public string Address { get; set; }

    public int ClientId { get; set; }
    public Client Client { get; set; }

    public void ValidateOnSave()
    {
        Guid.AssertValid();

        Ssn10?.AssertSsn10Valid();
        Orgnum?.AssertOrgnumValid();
    }
}
