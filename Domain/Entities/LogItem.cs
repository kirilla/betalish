namespace Betalish.Domain.Entities;

public class LogItem : ICreatedDateTime, IFormatOnSave
{
    public int Id { get; set; }

    public bool Error { get; set; }

    public string? Description { get; set; }
    public string? Exception { get; set; }
    public string? InnerException { get; set; }

    public LogItemKind? LogItemKind { get; set; }

    public int? ClientId { get; set; }
    public int? UserId { get; set; }
    public string? IpAddress { get; set; }

    public string? Source { get; set; }

    public DateTime? Created { get; set; }
    public DateTime? RepeatedUntil { get; set; }

    public int? RepeatCount { get; set; }
    
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

        Source = Source
            .Truncate(MaxLengths.Domain.LogItem.Source);
    }

    public LogItem()
    {
    }

    public LogItem(Exception ex)
    {
        Description = ex?.Message;
        Exception = ex?.Message;
        InnerException = ex?.InnerException?.Message;
    }

    public LogItem(IUserToken userToken)
    {
        ClientId = userToken.ClientId;
        UserId = userToken.UserId;
    }
}
