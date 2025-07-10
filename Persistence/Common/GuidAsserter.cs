using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Betalish.Persistence.Common;

public class GuidAsserter : IGuidAsserter
{
    public void AssertGuid(IEnumerable<EntityEntry> entries)
    {
        var entities = entries
            .Where(x => 
                x.State == EntityState.Added ||
                x.State == EntityState.Modified)
            .Select(x => x.Entity as IAssertGuid)
            .Where(x => x != null)
            .ToList();

        foreach (var entity in entities)
        {
            if (entity!.Guid == null)
                throw new NullGuidException();

            if (entity!.Guid == Guid.Empty)
                throw new EmptyGuidException();
        }
    }
}
