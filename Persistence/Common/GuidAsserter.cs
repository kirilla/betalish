using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Betalish.Persistence.Common;

public class GuidAsserter : IGuidAsserter
{
    public void AssertGuid(ChangeTracker changeTracker)
    {
        var entries = changeTracker
            .Entries()
            .Where(x => 
                x.State == EntityState.Added ||
                x.State == EntityState.Modified)
            .Select(x => x.Entity as IAssertGuid)
            .Where(x => x != null)
            .ToList();

        foreach (var entry in entries)
        {
            if (entry!.Guid == null)
                throw new NullGuidException();

            if (entry!.Guid == Guid.Empty)
                throw new EmptyGuidException();
        }
    }
}
