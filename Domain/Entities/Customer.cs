namespace Betalish.Domain.Entities;

public class Customer : IFormatOnSave, IValidateOnSave
{
    public int Id { get; set; }

    public Guid? Guid { get; set; }

    public CustomerKind CustomerKind { get; set; }

    public string Name { get; set; }

    public string? Ssn10 { get; set; }
    public string? Orgnum { get; set; }

    public string? EmailAddress { get; set; }

    public int ClientId { get; set; }
    public Client Client { get; set; }

    public void FormatOnSave()
    {
        Ssn10 = Ssn10?.StripNonNumeric();
        Orgnum = Orgnum?.StripNonNumeric();

        EmailAddress = EmailAddress?.Trim().ToLowerInvariant();
    }

    public void ValidateOnSave()
    {
        Guid.AssertValid();

        if (!Enum.IsDefined(CustomerKind))
            throw new InvalidEnumException();

        Ssn10?.AssertSsn10Valid();
        Orgnum?.AssertOrgnumValid();
    }
}
