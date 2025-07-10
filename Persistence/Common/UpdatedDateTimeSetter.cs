using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Betalish.Persistence.Common;

public class UpdatedDateTimeSetter : IUpdatedDateTimeSetter
{
    private readonly IDateService _dateService;

    public UpdatedDateTimeSetter(IDateService dateService)
    {
        _dateService = dateService;
    }

    public void SetUpdated(IEnumerable<EntityEntry> entries)
    {
        var entities = entries
            .Where(x =>
                x.State == EntityState.Added ||
                x.State == EntityState.Modified)
            .Select(x => x.Entity as IUpdatedDateTime)
            .Where(x => x != null)
            .ToList();

        foreach (var entity in entities)
        {
            entity!.Updated = _dateService.GetDateTimeNow();
        }
    }
}
