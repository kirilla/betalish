namespace Betalish.Domain.Entities;

public class SessionRecord
{
    public int Id { get; set; }

    public DateTime Login { get; set; }
    public DateTime Logout { get; set; }

    public bool WasReaped { get; set; }
    public bool WasForced { get; set; }

    public string? IpAddress { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public int? ClientId { get; set; }
    public Client? Client { get; set; }

    public string DateSummary =>
        $"{Login.ToDateOnly().ToIso8601()} - " +
        $"{Logout.ToDateOnly().ToIso8601()}";

    public string DateTimeSummary =>
        $"{Login.ToFixedFormatDateShortTime()} - " +
        $"{Logout.ToFixedFormatDateShortTime()}";

    public string Duration =>
        $"{(Logout - Login).ToSummary()}";
}
