namespace Betalish.Domain.Entities;

public class SessionActivity : ICreatedDateTime
{
    public int Id { get; set; }

    public DateTime? Created { get; set; }

    public int SessionId { get; set; }
    public Session Session { get; set; }
}
