namespace Betalish.Domain.Entities;

public class SessionRecord : IValidateOnSave
{
    public int Id { get; set; }

    public SignInBy? SignInBy { get; set; }

    public DateTime Login { get; set; }
    public DateTime Logout { get; set; }

    public SessionEnd SessionEnd { get; set; }

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

    public void ValidateOnSave()
    {
        if (SignInBy.HasValue &&
            !Enum.IsDefined(SignInBy.Value))
            throw new InvalidEnumException(nameof(SignInBy));

        if (!Enum.IsDefined(SessionEnd))
            throw new InvalidEnumException(nameof(SessionEnd));
    }
}
