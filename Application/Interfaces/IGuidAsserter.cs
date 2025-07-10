using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Betalish.Application.Interfaces;

public interface IGuidAsserter
{
    void AssertGuid(ChangeTracker changeTracker);
}
