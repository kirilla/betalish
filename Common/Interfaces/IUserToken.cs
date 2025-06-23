namespace Betalish.Common.Interfaces;

public interface IUserToken
{
    int? UserId { get; }
    int? SessionId { get; }

    string? Name { get; }

    bool IsAuthenticated { get; }
}
