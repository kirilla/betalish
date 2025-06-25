using System.Net;

namespace Betalish.Application.Queues.BadSignIns;

public interface IBadSignInList
{
    void AddSignIn(
        IPAddress? ipAddress,
        string? name,
        string? password,
        string? exceptionMsg,
        string? innerExceptionMsg);

    List<BadSignIn> TakeBadSignIns();
}
