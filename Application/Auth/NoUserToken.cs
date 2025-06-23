namespace Betalish.Application.Auth;

public class NoUserToken : IUserToken
{
    public int? UserId { get; }
    public int? SessionId { get; }

    public string? Name { get; }

    public bool IsAuthenticated { get; }

    public int? ClientId { get; }
    public string? ClientName { get; }

    public NoUserToken()
    {
        UserId = null;
        SessionId = null;

        Name = null;

        IsAuthenticated = false;

        ClientId = null;
        ClientName = null;
    }
}
