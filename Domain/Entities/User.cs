using Betalish.Common.Logic;

namespace Betalish.Domain.Entities;

public class User : 
    ICreatedDateTime, 
    IUpdatedDateTime, 
    IFormatOnSave, 
    IValidateOnSave
{
    public int Id { get; set; }

    public string Ssn12 { get; set; }
    public string Ssn10 { get; set; }

    public DateOnly? SsnDate { get; set; }

    public string Name { get; set; }

    public string PasswordHash { get; set; }

    public Guid? Guid { get; set; }

    public bool NoLogin { get; set; }
    public bool NoSave { get; set; }

    public DateTime? Created { get; set; }
    public DateTime? Updated { get; set; }

    public List<AdminAuth> AdminAuths { get; set; }
    public List<ClientAuth> ClientAuths { get; set; }
    public List<ClientEvent> ClientEvents { get; set; }
    public List<Session> Sessions { get; set; }
    public List<UserEmail> UserEmails { get; set; }
    public List<UserEvent> UserEvents { get; set; }
    public List<UserPhone> UserPhones { get; set; }

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
