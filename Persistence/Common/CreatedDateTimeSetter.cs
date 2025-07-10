using Betalish.Common.Dates;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Betalish.Persistence.Common;

public class CreatedDateTimeSetter : ICreatedDateTimeSetter
{
    private readonly IDateService _dateService;

    public CreatedDateTimeSetter(IDateService dateService)
    {
        _dateService = dateService;
    }

    public void SetCreated(IEnumerable<EntityEntry> entries)
    {
        var entities = entries
            .Where(x => x.State == EntityState.Added)
            .Select(x => x.Entity as ICreatedDateTime)
            .Where(x =>
                x != null &&
                x.Created == null)
            .ToList();

        foreach (var entity in entities)
        {
            entity!.Created = _dateService.GetDateTimeNow();
        }
    }
}
