namespace Betalish.Domain.Entities;

public class Customer : IFormatOnSave, IValidateOnSave
{
    public int Id { get; set; }

    public Guid? Guid { get; set; }

    public CustomerKind CustomerKind { get; set; }

    public required string Name { get; set; }

    public string? Ssn10 { get; set; }
    public string? Orgnum { get; set; }

    public string? EmailAddress { get; set; }

    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;

    public List<CustomerTag> CustomerTags { get; set; } = [];
    public List<InvoiceDraft> InvoiceDrafts { get; set; } = [];

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

        if (CustomerKind == CustomerKind.Person &&
            Orgnum.HasValue())
            throw new ValidateOnSaveException(
                $"Customer {Name} should not have an Orgnum.");

        if (CustomerKind == CustomerKind.Organization &&
            Ssn10.HasValue())
            throw new ValidateOnSaveException(
                $"Customer {Name} should not have an Ssn10.");
    }
}
