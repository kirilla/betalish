namespace Betalish.Domain.Entities;

public class Session : ICreatedDateTime, IUpdatedDateTime
{
    public int Id { get; set; }

    public Guid? Guid { get; set; }

    public DateTime? Created { get; set; }
    public DateTime? Updated { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public int? ClientId { get; set; }
    public Client? Client { get; set; }

    public List<SessionActivity> SessionActivities { get; set; }

    public string DateSummary =>
        $"{Created.ToDateOnly().ToIso8601()} - " +
        $"{Updated.ToDateOnly().ToIso8601()}";

    public string DateTimeSummary =>
        $"{Created.ToFixedFormatDateShortTime()} - " +
        $"{Updated.ToFixedFormatDateShortTime()}";
}
