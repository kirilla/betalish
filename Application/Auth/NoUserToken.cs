namespace Betalish.Application.Auth;

public class NoUserToken : IUserToken
{
    public int? UserId { get; }
    public int? SessionId { get; }

    public string? Name { get; }

    public bool IsAuthenticated { get; }

    public NoUserToken()
    {
        UserId = null;
        SessionId = null;

        Name = null;

        IsAuthenticated = false;
    }
}
