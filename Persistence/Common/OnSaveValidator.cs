using Betalish.Common.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Betalish.Persistence.Common;

public class OnSaveValidator : IOnSaveValidator
{
    public OnSaveValidator()
    {
    }

    public void Validate(ChangeTracker changeTracker)
    {
        var entries = changeTracker
            .Entries()
            .Where(x =>
                x.State == EntityState.Added ||
                x.State == EntityState.Modified)
            .Select(x => x.Entity as IValidateOnSave)
            .Where(x => x != null)
            .ToList();

        foreach (var entry in entries)
        {
            entry!.ValidateOnSave();
        }
    }
}
