namespace Betalish.Domain.Entities;

public class LogItem : ICreatedDateTime, IFormatOnSave
{
    public int Id { get; set; }

    public bool Error { get; set; }

    public string? Description { get; set; }
    public string? Exception { get; set; }
    public string? InnerException { get; set; }

    public LogItemKind? LogItemKind { get; set; }

    public int? UserId { get; set; }
    public string? IpAddress { get; set; }

    public DateTime? Created { get; set; }

    public void FormatOnSave()
    {
        Description = Description
            .Truncate(MaxLengths.Domain.LogItem.Description);

        Exception = Exception?
            .Truncate(MaxLengths.Domain.LogItem.Exception);

        InnerException = InnerException
            .Truncate(MaxLengths.Domain.LogItem.InnerException);

        IpAddress = IpAddress
            .Truncate(MaxLengths.Common.Ip.Address.IPv6);
    }
}
