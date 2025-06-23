namespace Betalish.Domain.Entities;

public class AdminAuth
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }
}
