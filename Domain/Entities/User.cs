namespace Betalish.Domain.Entities;

public class User : ICreatedDateTime, IUpdatedDateTime
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string PasswordHash { get; set; }

    public Guid? Guid { get; set; }

    public bool NoLogin { get; set; }
    public bool NoSave { get; set; }

    public DateTime? Created { get; set; }
    public DateTime? Updated { get; set; }

    public List<AdminAuth> AdminAuths { get; set; }
    public List<ClientAuth> ClientAuths { get; set; }
    public List<Session> Sessions { get; set; }
    public List<UserEmail> UserEmails { get; set; }
}
