namespace Betalish.Domain.Entities;

public class ClientEvent : ICreatedDateTime, IFormatOnSave
{
    public int Id { get; set; }

    public ClientEventKind? ClientEventKind { get; set; }

    public string? Description { get; set; }
    public string? IpAddress { get; set; }

    public DateTime? Created { get; set; }

    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;

    public int? UserId { get; set; }
    public User? User { get; set; }

    public void FormatOnSave()
    {
        Description = Description?[..MaxLengths.Domain.LogItem.Description];
        IpAddress = IpAddress?[..MaxLengths.Common.Ip.Address.IPv6];
    }
}
