namespace Betalish.Application.Auth;

public class NoUserToken : IUserToken
{
    public int? UserId { get; }
    public int? SessionId { get; }

    public string? Name { get; }

    public bool IsAdmin { get; }
    public bool IsAuthenticated { get; }
    public bool IsClient { get; }

    public int? ClientId { get; }
    public string? ClientName { get; }

    public bool NoLogin { get; }
    public bool NoSave { get; set; }

    public NoUserToken()
    {
        UserId = null;
        SessionId = null;

        Name = null;

        IsAdmin = false;
        IsAuthenticated = false;
        IsClient = false;

        ClientId = null;
        ClientName = null;

        NoLogin = false;
        NoSave = false;
    }
}
