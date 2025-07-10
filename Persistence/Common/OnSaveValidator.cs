using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Betalish.Persistence.Common;

public class OnSaveValidator : IOnSaveValidator
{
    public OnSaveValidator()
    {
    }

    public void Validate(IEnumerable<EntityEntry> entries)
    {
        var entities = entries
            .Where(x =>
                x.State == EntityState.Added ||
                x.State == EntityState.Modified)
            .Select(x => x.Entity as IValidateOnSave)
            .Where(x => x != null)
            .ToList();

        foreach (var entity in entities)
        {
            entity!.ValidateOnSave();
        }
    }
}
