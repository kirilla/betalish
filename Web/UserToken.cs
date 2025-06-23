namespace Betalish.Web;

public class UserToken : IUserToken
{
    private readonly IHttpContextAccessor? _httpContext;

    public int? UserId { get; }
    public int? SessionId { get; }

    public string? Name { get; }

    public bool IsAuthenticated { get; }

    public int? ClientId { get; }
    public string? ClientName { get; }

    public UserToken(IHttpContextAccessor httpContext)
    {
        _httpContext = httpContext;

        UserId = _httpContext?.HttpContext?.Items?["UserId"] as int?;
        SessionId = _httpContext?.HttpContext?.Items?["SessionId"] as int?;

        Name = _httpContext?.HttpContext?.Items?["UserName"] as string;

        IsAuthenticated = 
            UserId.HasValue && 
            SessionId.HasValue;

        ClientId = _httpContext?.HttpContext?.Items?["ClientId"] as int?;
        ClientName = _httpContext?.HttpContext?.Items?["ClientName"] as string;
    }
}
