using Betalish.Common.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Betalish.Persistence.Common;

public class OnSaveFormatter : IOnSaveFormatter
{
    public OnSaveFormatter()
    {
    }

    public void Format(ChangeTracker changeTracker)
    {
        var entries = changeTracker
            .Entries()
            .Where(x =>
                x.State == EntityState.Added ||
                x.State == EntityState.Modified)
            .Select(x => x.Entity as IFormatOnSave)
            .Where(x => x != null)
            .ToList();

        foreach (var entry in entries)
        {
            entry!.FormatOnSave();
        }
    }
}
