using Betalish.Common.Dates;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Betalish.Persistence.Common;

public class UpdatedDateTimeSetter : IUpdatedDateTimeSetter
{
    private readonly IDateService _dateService;

    public UpdatedDateTimeSetter(IDateService dateService)
    {
        _dateService = dateService;
    }

    public void SetUpdated(ChangeTracker changeTracker)
    {
        var entries = changeTracker
            .Entries()
            .Where(x =>
                x.State == EntityState.Added ||
                x.State == EntityState.Modified)
            .Select(x => x.Entity as IUpdatedDateTime)
            .Where(x => x != null)
            .ToList();

        foreach (var entry in entries)
        {
            entry!.Updated = _dateService.GetDateTimeNow();
        }
    }
}
