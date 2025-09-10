namespace Betalish.Domain.Entities;

public class UserEvent : ICreatedDateTime, IFormatOnSave
{
    public int Id { get; set; }

    public UserEventKind? UserEventKind { get; set; }

    public string? Description { get; set; }
    public string? IpAddress { get; set; }

    public DateTime? Created { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public void FormatOnSave()
    {
        Description = Description
            .Truncate(MaxLengths.Domain.UserEvent.Description);

        IpAddress = IpAddress
            .Truncate(MaxLengths.Common.Ip.Address.IPv6);
    }
}
