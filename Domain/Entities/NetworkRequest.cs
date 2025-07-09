namespace Betalish.Domain.Entities;

public class NetworkRequest : ICreatedDateTime
{
    public int Id { get; set; }

    public DateTime? Created { get; set; }

    public string? Url { get; set; }
    public string? Method { get; set; }

    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }

    public bool Blocked { get; set; }

    public int? RepeatCount { get; set; }
}
