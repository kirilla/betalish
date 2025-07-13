namespace Betalish.Domain.Entities;

public class User : 
    ICreatedDateTime, 
    IUpdatedDateTime, 
    IFormatOnSave, 
    IValidateOnSave
{
    public int Id { get; set; }

    public required string Name { get; set; }
    public required string PasswordHash { get; set; }

    public Guid? Guid { get; set; }

    public bool NoLogin { get; set; }
    public bool NoSave { get; set; }

    public UserPurpose UserPurpose { get; set; }

    public DateTime? Created { get; set; }
    public DateTime? Updated { get; set; }

    public List<AdminAuth> AdminAuths { get; set; } = [];
    public List<ClientAuth> ClientAuths { get; set; } = [];
    public List<ClientEvent> ClientEvents { get; set; } = [];
    public List<Session> Sessions { get; set; } = [];
    public List<UserEmail> UserEmails { get; set; } = [];
    public List<UserEvent> UserEvents { get; set; } = [];
    public List<UserPhone> UserPhones { get; set; } = [];
    public List<UserSsn> UserSsns { get; set; } = [];

    public void FormatOnSave()
    {
    }

    public void ValidateOnSave()
    {
        if (string.IsNullOrWhiteSpace(PasswordHash))
            throw new MissingPasswordHashException();

        if (!Enum.IsDefined(UserPurpose))
            throw new InvalidEnumException();
    }
}
