namespace Betalish.Common.Interfaces;

public interface IUserToken
{
    int? UserId { get; }
    int? SessionId { get; }

    string? Name { get; }

    bool IsAuthenticated { get; }

    int? ClientId { get; }
    string? ClientName { get; }

    bool NoLogin { get; set; }
    bool NoSave { get; set; }
}
