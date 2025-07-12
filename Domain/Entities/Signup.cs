namespace Betalish.Domain.Entities;

public class Signup :
    ICreatedDateTime, 
    IUpdatedDateTime, 
    IFormatOnSave, 
    IValidateOnSave
{
    public int Id { get; set; }

    public Guid? Guid { get; set; }

    // Person
    public required string Ssn12 { get; set; }
    public required string PersonName { get; set; }
    public required string EmailAddress { get; set; }
    public required string PhoneNumber { get; set; }

    public DateTime? Created { get; set; }
    public DateTime? Updated { get; set; }

    public void FormatOnSave()
    {
        EmailAddress = EmailAddress.Trim().ToLowerInvariant();
        PhoneNumber = PhoneNumber.StripNonPhoneNumberChars();

        Ssn12 = Ssn12.StripNonNumeric();

        this.SetEmptyStringsToNull();
    }

    public void ValidateOnSave()
    {
        ValidateEmailAddress();
        ValidateSsn12();
    }

    private void ValidateEmailAddress()
    {
        if (string.IsNullOrWhiteSpace(EmailAddress))
            throw new MissingEmailException();

        if (!RegexLogic.IsMatch(EmailAddress, Pattern.Common.Email.Address))
            throw new InvalidEmailException();
    }

    private void ValidateSsn12()
    {
        if (string.IsNullOrWhiteSpace(Ssn12))
            throw new MissingSsnException();

        if (!RegexLogic.IsMatch(Ssn12, Pattern.Common.Ssn.Ssn12))
            throw new InvalidSsnException();

        if (!SsnLogic.IsValidSsn12(Ssn12))
            throw new InvalidSsnException();
    }
}
