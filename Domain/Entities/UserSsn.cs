namespace Betalish.Domain.Entities;

public class UserSsn : 
    ICreatedDateTime, IUpdatedDateTime, IFormatOnSave, IValidateOnSave
{
    public int Id { get; set; }

    public string Ssn12 { get; set; }
    public string Ssn10 { get; set; }

    public DateOnly? SsnDate { get; set; }

    public DateTime? Created { get; set; }
    public DateTime? Updated { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public void FormatOnSave()
    {
        Ssn12 = Ssn12.StripNonNumeric();
        Ssn10 = Ssn12.ToSsn10();

        SsnDate = Ssn12.ToDateOnly();
    }

    public void ValidateOnSave()
    {
        ValidateSsn10();
        ValidateSsn12();

        // Ignore when SsnDate == null.
        // Some SSN do not map to valid dates,
        // e.g. samordningsnummer.
    }

    private void ValidateSsn10()
    {
        if (string.IsNullOrWhiteSpace(Ssn10))
            throw new MissingSsnException();

        if (!RegexLogic.IsMatch(Ssn10, Pattern.Common.Ssn.Ssn10))
            throw new InvalidSsnException();

        if (!SsnLogic.IsValidSsn10(Ssn10))
            throw new InvalidSsnException();
    }

    private void ValidateSsn12()
    {
        if (string.IsNullOrWhiteSpace(Ssn12))
            throw new MissingSsnException();

        if (!RegexLogic.IsMatch(Ssn12, Pattern.Common.Ssn.Ssn12))
            throw new InvalidSsnException();

        if (!SsnLogic.IsValidSsn10(Ssn12))
            throw new InvalidSsnException();
    }
}
